using System.ComponentModel.DataAnnotations;

namespace Tutorial11.Showcase.Models;

public class Post
{
    public int PostId { get; set; }
    public required string Title { get; set; }
    public required string Content { get; set; }
    
    [Timestamp]
    public byte[] ConcurrencyToken { get; set; }

    public int BlogId { get; set; }
    public Blog Blog { get; set; }
}