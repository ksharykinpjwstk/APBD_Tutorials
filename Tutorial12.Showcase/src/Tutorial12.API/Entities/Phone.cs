using System.ComponentModel.DataAnnotations;

namespace Tutorial12.API.Entities;

public class Phone
{
    public int Id { get; set; }
    
    public required string ModelName { get; set; }
    
    public int CoreCount { get; set; }
    
    public int Ram { get; set; }
    
    public bool Has5G { get; set; }
    
    public string? Description { get; set; }
    
    [Timestamp]
    public byte[] ConcurrencyToken { get; set; }  = null!;
    
    // One - to - many (dependent child)
    // Here we have navigation only to parent (or so called "principal")
    public int PhoneManufactureId { get; set; }
    public PhoneManufacture PhoneManufacture { get; set; }
}