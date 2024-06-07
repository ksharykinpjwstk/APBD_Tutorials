using System.ComponentModel.DataAnnotations;

namespace Tutorial12.API.Entities;

public class PhoneManufacture
{
    public int Id { get; set; }
    
    public required string Name { get; set; }

    [Timestamp] public byte[] ConcurrencyToken { get; set; } = null!;
}