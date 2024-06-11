using Tutorial12.API.Entities;

namespace Tutorial12.API.Services;

public interface IAuthenticationService
{
    string GenerateAccessToken(User user);
    string GenerateRefreshToken();
    Task<bool> ValidateExpiredAccessTokenAsync(string accessToken);
}