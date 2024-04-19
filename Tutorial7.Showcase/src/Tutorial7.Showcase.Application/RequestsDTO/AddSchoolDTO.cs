using System.ComponentModel.DataAnnotations;

namespace Tutorial7.Showcase.Application;

public class AddSchoolDTO
{
    [Required]
    public int CityId {get; set;}

    [Required]
    [Length(0, 200)]
    public required string Name {get; set;}

    [Required]
    public int StudentCount {get; set;}

    [Length(0, 2000)]
    public string? Description {get; set;}
}
