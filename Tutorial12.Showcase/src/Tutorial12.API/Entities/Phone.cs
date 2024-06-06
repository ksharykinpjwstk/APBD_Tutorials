namespace Tutorial12.API.Entities;

public class Phone
{
    public int Id { get; set; }
    
    public required string ModelName { get; set; }
    
    public int CoreCount { get; set; }
    
    public int Ram { get; set; }
    
    public bool Has5G { get; set; }
    
    public string? Description { get; set; }
    
    public int PhoneManufactureId { get; set; }
}