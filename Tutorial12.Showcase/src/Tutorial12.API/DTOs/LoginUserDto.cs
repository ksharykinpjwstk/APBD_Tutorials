using System.ComponentModel.DataAnnotations;
using Tutorial12.API.Entities;

namespace Tutorial12.API.DTOs;

public class LoginUserDto
{
    [Required]
    public required string Username { get; set; }
    [Required]
    public required string Password { get; set; }
}