namespace Tutorial7.Showcase.Application;

//public record SchoolDTO(string Country, string City, string Name, int StudentCount, string? Description);

public record SchoolDTO 
{
    /// <summary>
    /// Name of the country, where school is located.
    /// </summary>
    public required string Country {get; init;}

    /// <summary>
    /// Name of the city, where school is located
    /// </summary>
    public required string City {get; init;}

    /// <summary>
    /// Name of the school
    /// </summary>
    public required string Name {get; init;}

    /// <summary>
    /// Count of students that studies in the school
    /// </summary>
    public required int StudentCount {get; init;}

    /// <summary>
    /// Description of the school. Optional parameter.
    /// </summary>
    public string? Description {get; init;}
}