namespace Tutorial12.API.Entities;

public class User
{
    public int Id { get; set; }
    
    public required string Username { get; set; }
    
    public required string Password { get; set; }
    
    public string? RefreshToken { get; set; }
    
    public DateTime RefreshTokenExpire { get; set; }
}