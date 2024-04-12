using System.Text.Json.Serialization;

namespace Tutorial6.Showcase.Json.Models;

public class University
{
    [JsonPropertyName("name")]
    public required string Name { get; set; }
    [JsonPropertyName("startYear")]
    public required int StartYear { get; set; }
    [JsonPropertyName("endYear")]
    public required int EndYear { get; set; }
}