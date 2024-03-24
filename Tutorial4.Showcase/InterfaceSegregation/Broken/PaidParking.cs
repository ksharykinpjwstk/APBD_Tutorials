using Tutorial4.Showcase.InterfaceSegregation.Broken;

public class PaidParking : IBrokenParking
{
    private readonly Dictionary<int, Car> _parkingLots = new();
    public int AvailableSlots {get; private set;} = 100;
    public string GenerateTicket()
    {
        return "some ticket ID";
    }

    public void GetPayment()
    {
        //Some processing payment logic 
    }

    public void Park(Car car)
    {
        //Some parking logic
        AvailableSlots--;
    }

    public void Unpark(Car car)
    {
        //Some unparking logic
        AvailableSlots++;
    }
}