using System.ComponentModel.DataAnnotations;

namespace Tutorial11.Showcase.Models;

public class Blog
{
    public int BlogId { get; set; }
    public string Url { get; set; }

    [Timestamp]
    public byte[] ConcurrencyToken { get; set; }
    public List<Post> Posts { get; } = new();
}