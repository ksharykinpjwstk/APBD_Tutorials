namespace Tutorial7.Showcase.Entities;

public class School
{
    public int Id {get; set;}

    public int CityId {get; set;}

    public required string Name {get; set;}

    public int StudentCoutn {get; set;}

    public string? Description {get; set;}
}
