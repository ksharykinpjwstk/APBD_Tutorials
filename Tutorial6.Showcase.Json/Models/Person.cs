using System.Text;
using System.Text.Json.Serialization;

namespace Tutorial6.Showcase.Json.Models;

public class Person
{
    [JsonPropertyName("name")]
    public required string Name { get; init; }
    [JsonPropertyName("jobs")]
    public required List<string> Jobs { get; init; }
    
    [JsonPropertyName("universities")]
    public List<University>? Universities { get; init; }
    [JsonPropertyName("favouriteGame")]
    public Game? FavouriteGame { get; init; }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        var isStudied = !(Universities is null || Universities.Count == 0);

        sb.Append($"My name is {Name}. My jobs are {string.Join(", ", Jobs)}. ");
        sb.Append(isStudied ? $"My universities are {string.Join(", ", Universities.Select(u => u.Name))}. " : "I've never studied in universities. ");
        sb.Append(FavouriteGame is null ? "I don't have favourite game" : $"My favourite game is {FavouriteGame.Name}");
        return sb.ToString();
    }
}