using Tutorial12.API.Entities;

namespace Tutorial12.API.DTOs.Phones;

public class PhoneDto
{
    public PhoneDto(Phone phone)
    {
        Manufacture = phone.PhoneManufacture.Name;
        ModelName = phone.ModelName;
        CoreCount = phone.CoreCount;
        Ram = phone.Ram;
        Has5G = phone.Has5G;
        Description = phone.Description;
    }
    
    public string Manufacture { get; set; }
    public string ModelName { get; set; }
    public int CoreCount { get; set; }
    
    public int Ram { get; set; }
    
    public bool Has5G { get; set; }
    
    public string? Description { get; set; }    
}