namespace Tutorial4.Showcase.OpenClosed.Broken;

public class AreaCalculatorService
{
    public double CalculateSum(Rectangle[] rectangles)
    {
        double sumArea = 0;
        
        foreach (var rectangle in rectangles)
        {
            sumArea += rectangle.Height * rectangle.Width;
        }
        // What if triangle???? Other shapes???
        return sumArea;
    }
}