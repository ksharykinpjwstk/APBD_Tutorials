namespace Tutorial12.API.Services;

public interface IAuthenticationService
{
    string GenerateAccessToken(string username);
    string GenerateRefreshToken();
    Task<bool> ValidateExpiredAccessTokenAsync(string accessToken);
}