using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tutorial12.API.DTOs;
using Tutorial12.API.Entities;
using Tutorial12.API.Helpers;
using Tutorial12.API.Services;

namespace Tutorial12.API.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController(ApplicationContext context, IAuthenticationService authService) : ControllerBase
{
    private readonly PasswordHasher<User> _passwordHasher = new();

    /// <summary>
    /// Create a new user
    /// </summary>
    /// <param name="createUser">Class instance that contains basic info about user and their credentials.</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Nothing</returns>
    [Route("register")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserDto createUser,
        CancellationToken cancellationToken)
    {
        var mappedUser = createUser.Map();
        mappedUser.Password = _passwordHasher.HashPassword(mappedUser, mappedUser.Password);
        mappedUser.RoleId = context.Roles.First(r => r.Name == "User").Id;
        context.Users.Add(mappedUser);
        await context.SaveChangesAsync(cancellationToken);
        return Created();
    }

    /// <summary>
    /// Login user with given credentials.
    /// </summary>
    /// <param name="loginUser">Class that describes credentials.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Access and refresh tokens.</returns>
    [Route("login")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<AuthDto>> LoginUser([FromBody] LoginUserDto loginUser,
        CancellationToken cancellationToken)
    {
        var databaseUser = await context.Users.Include(u => u.Role).FirstOrDefaultAsync(
            u => u.Username == loginUser.Username,
            cancellationToken: cancellationToken);
        if (databaseUser is null)
        {
            return Unauthorized();
        }

        var verificationResult =
            _passwordHasher.VerifyHashedPassword(databaseUser, databaseUser.Password, loginUser.Password);
        if (verificationResult == PasswordVerificationResult.Failed)
        {
            return Unauthorized();
        }

        var authResponse = new AuthDto
        {
            AccessToken = authService.GenerateAccessToken(databaseUser),
            RefreshToken = authService.GenerateRefreshToken()
        };

        databaseUser.RefreshToken = authResponse.RefreshToken;
        // Refresh token is valid for 1 day.
        databaseUser.RefreshTokenExpire = DateTime.Now.AddDays(1);

        context.Entry(databaseUser).State = EntityState.Modified;
        context.Users.Update(databaseUser);
        await context.SaveChangesAsync(cancellationToken);

        return Ok(authResponse);
    }

    /// <summary>
    /// Provide new access and refresh tokens.
    /// </summary>
    /// <param name="auth">Class describes refresh and access tokens.</param>
    /// <param name="cancellationToken">Cancellation tokens.</param>
    /// <returns>Refreshed accessed and refresh tokens.</returns>
    [Route("refresh")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<AuthDto>> RefreshToken([FromBody] AuthDto auth, CancellationToken cancellationToken)
    {
        var isTokenValid = await authService.ValidateExpiredAccessTokenAsync(auth.AccessToken);
        if (isTokenValid is false)
        {
            return Forbid();
        }

        // We search user by refresh token. Not the best approach, but for this example is OK.
        var currentUser = await context.Users.Include(u => u.Role)
            .FirstOrDefaultAsync(u => u.RefreshToken == auth.RefreshToken, cancellationToken: cancellationToken);
        if (currentUser is null || currentUser.RefreshTokenExpire < DateTime.Now)
        {
            return Forbid();
        }

        var authResponse = new AuthDto
        {
            AccessToken = authService.GenerateAccessToken(currentUser),
            RefreshToken = authService.GenerateRefreshToken()
        };

        currentUser.RefreshToken = authResponse.RefreshToken;
        currentUser.RefreshTokenExpire = DateTime.Now.AddDays(1);

        context.Entry(currentUser).State = EntityState.Modified;
        context.Users.Update(currentUser);
        await context.SaveChangesAsync(cancellationToken);

        return Ok(authResponse);
    }
}