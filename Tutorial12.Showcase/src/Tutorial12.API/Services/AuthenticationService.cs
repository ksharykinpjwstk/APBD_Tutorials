using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Tutorial12.API.Entities;

namespace Tutorial12.API.Services;

public class AuthenticationService(IConfiguration config) : IAuthenticationService
{
    // Access token valid for 10 minutes.
    private static readonly TimeSpan validTime = TimeSpan.FromMinutes(10);
    public string GenerateAccessToken(User user)
    {
        // We need it for token creation
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(config["JwtSettings:Key"]!);

        // Claim is a property in token that describes some information about token or user.
        var claims = new List<Claim>
        {
            // Jti - kinda "id" of token
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            // Sub - is used for usernames.
            new(JwtRegisteredClaimNames.Sub, user.Username),
            new(type: "role", user.Role.Name)
        };

        // Set up basic token information for generation.
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.Add(validTime),
            Issuer = config["JwtSettings:Issuer"],
            Audience = config["JwtSettings:Audience"],
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
        };

        // Create token
        var token = tokenHandler.CreateToken(tokenDescriptor);
        // Serialise token to string
        var jwt = tokenHandler.WriteToken(token);
        return jwt;
    }

    /// <summary>
    /// Generate random bytes of refresh token.
    /// </summary>
    /// <returns>Random value as string</returns>
    public string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);

        return Convert.ToBase64String(randomNumber);
    }

    public async Task<bool> ValidateExpiredAccessTokenAsync(string accessToken)
    {
        var key = Encoding.UTF8.GetBytes(config["JwtSettings:Key"]!);
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            // We need to check only key, so every other validation is disabled.
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateLifetime = false
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        
        var principal = await tokenHandler.ValidateTokenAsync(accessToken, tokenValidationParameters);
        return principal.IsValid;
    }
}