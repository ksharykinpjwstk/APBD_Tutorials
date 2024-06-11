using Tutorial12.API.Entities;

namespace Tutorial12.API.Services;

public interface IAuthenticationService
{
    /// <summary>
    /// Generates access token string. This access token can be used for accessing protected endpoints in Phone controller.
    /// </summary>
    /// <param name="user">Current user</param>
    /// <returns>Access token as string</returns>
    string GenerateAccessToken(User user);
    /// <summary>
    /// Generate refresh token. This is used for getting new access token, if there any reason that make impossible to use access token.
    /// </summary>
    /// <returns>Refresh token as string.</returns>
    string GenerateRefreshToken();
    /// <summary>
    /// Validate if access token is expired.
    /// </summary>
    /// <param name="accessToken">Current access token</param>
    /// <returns>Boolean result of validation</returns>
    Task<bool> ValidateExpiredAccessTokenAsync(string accessToken);
}