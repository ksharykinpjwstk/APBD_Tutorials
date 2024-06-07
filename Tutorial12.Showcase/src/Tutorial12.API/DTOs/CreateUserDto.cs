using System.ComponentModel.DataAnnotations;
using Tutorial12.API.Entities;

namespace Tutorial12.API.DTOs;

public class CreateUserDto
{
    [Required]
    [Length(8, 50)]
    public required string Username { get; set; }
    
    //Also good to make some symbol requirements
    [Required]
    [Length(8, 32)]
    public required string Password { get; set; }

    public User Map()
    {
        return new User
        {
            Username = Username,
            Password = Password
        };
    }
}