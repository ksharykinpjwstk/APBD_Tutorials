using System.ComponentModel.DataAnnotations;

namespace Tutorial6.Showcase.Models;

public class Car
{
    public int Id { get; set; }
    
    [Required]
    [Length(3, 50)]
    public string Name { get; set; }
    
    [Required]
    [Range(0, double.MaxValue)]
    public double Weight { get; set; }
    
    [Required]
    [Range(0, 1000)]
    public double TopSpeed { get; set; }
}