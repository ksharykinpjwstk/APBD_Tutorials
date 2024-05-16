namespace Tutorial10.RestApi.Models;

public class Car
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public double Weight { get; set; }

    public double TopSpeed { get; set; }
}
