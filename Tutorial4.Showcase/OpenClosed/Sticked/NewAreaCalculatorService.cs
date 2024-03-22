namespace Tutorial4.Showcase.OpenClosed.Sticked;

public class NewAreaCalculatorService
{
    public double CalculateSum(Figure[] figures)
    {
        double sum = 0;

        foreach (var figure in figures)
        {
            sum += figure.calculateArea();
        }

        return sum;
    }
}