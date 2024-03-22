namespace Tutorial4.Showcase.OpenClosed.Sticked;

public class Rectangle : Figure
{
    public double Height { get; set; }
    public double Width { get; set; }
    
    public double calculateArea()
    {
        return Height * Width;
    }
}