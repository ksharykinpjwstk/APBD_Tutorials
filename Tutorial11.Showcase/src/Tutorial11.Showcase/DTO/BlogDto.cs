namespace Tutorial11.Showcase.DTO;

public class BlogDto
{
    public required string Url { get; set; }
    
    public List<PostDto>? Posts { get; set; }
}