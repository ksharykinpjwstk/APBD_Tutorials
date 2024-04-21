using System.ComponentModel.DataAnnotations;

namespace Tutorial7.Showcase.Application;

public class AddSchoolDTO
{
    /// <summary>
    /// ID of city where school is located. 
    /// </summary>
    /// <example>1</example>
    [Required]
    public int CityId {get; set;}

    /// <summary>
    /// Name of the school
    /// </summary>
    /// <example>Primary school No. 321</example>
    [Required]
    [Length(0, 200)]
    public required string Name {get; set;}

    /// <summary>
    /// Count of student in school
    /// </summary>
    /// <example>460</example>
    [Required]
    public int StudentCount {get; set;}

    /// <summary>
    /// Description of the school.
    /// </summary>
    /// <example>The first students started studying at Primary School No. 321 in September 1984.</example>
    [Length(0, 2000)]
    public string? Description {get; set;}
}
