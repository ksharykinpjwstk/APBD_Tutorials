using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tutorial12.API.DTOs;
using Tutorial12.API.Entities;
using Tutorial12.API.Services;

namespace Tutorial12.API.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController(ApplicationContext context, IAuthenticationService authService) : ControllerBase
{
    private readonly PasswordHasher<User> _passwordHasher = new();
    [Route("register")]
    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserDto createUser, CancellationToken cancellationToken)
    {
        var mappedUser = createUser.Map();
        mappedUser.Password = _passwordHasher.HashPassword(mappedUser, mappedUser.Password);
        context.Users.Add(mappedUser);
        await context.SaveChangesAsync(cancellationToken);
        return Created();
    }

    [Route("login")]
    [HttpPost]
    public async Task<ActionResult<AuthDto>> LoginUser([FromBody] LoginUserDto loginUser, CancellationToken cancellationToken)
    {
        var databaseUser = await context.Users.FirstOrDefaultAsync(u => u.Username == loginUser.Username, cancellationToken: cancellationToken);
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
            AccessToken = authService.GenerateAccessToken(databaseUser.Username),
            RefreshToken = authService.GenerateRefreshToken()
        };
        
        databaseUser.RefreshToken = authResponse.RefreshToken;
        databaseUser.RefreshTokenExpire = DateTime.Now.AddDays(1);
        
        context.Entry(databaseUser).State = EntityState.Modified;
        context.Users.Update(databaseUser);
        await context.SaveChangesAsync(cancellationToken);
        
        return Ok(authResponse);
    }

    [Route("refresh")]
    [HttpPost]
    public async Task<ActionResult<AuthDto>> RefreshToken([FromBody] AuthDto auth, CancellationToken cancellationToken)
    {
        var isTokenValid = await authService.ValidateExpiredAccessTokenAsync(auth.AccessToken);
        if (isTokenValid is false)
        {
            return Forbid();
        }

        var currentUser = context.Users.FirstOrDefault(u => u.RefreshToken == auth.RefreshToken);
        if (currentUser is null || currentUser.RefreshTokenExpire < DateTime.Now)
        {
            return Forbid();
        }
        
        var authResponse = new AuthDto
        {
            AccessToken = authService.GenerateAccessToken(currentUser.Username),
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