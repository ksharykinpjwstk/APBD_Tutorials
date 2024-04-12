using System.Text.Json.Serialization;

namespace Tutorial6.Showcase.Json.Models;

public class Game
{
    [JsonPropertyName("name")]
    public required string Name { get; set; }
    [JsonPropertyName("releaseYear")]
    public required int ReleaseYear { get; set; }
}