namespace Tutorial4.Showcase.OpenClosed.Sticked;

public class Triangle : Figure
{
    public double Height { get; init; }
    public double Base { get; init; }
    private const double Half = 0.5;
    public double calculateArea()
    {
        return Half * Height * Base;
    }
}