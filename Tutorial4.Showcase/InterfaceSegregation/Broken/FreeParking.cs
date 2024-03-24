using Tutorial4.Showcase.InterfaceSegregation.Broken;

public class FreeParking : IBrokenParking
{
    private readonly Dictionary<int, Car> _parkingLots = new();
    public int AvailableSlots {get; private set;} = 100;
    // Broken Liskov and Interface segregation principles
    public string GenerateTicket()
    {
        throw new NotImplementedException();
    }
    // Broken Liskov and Interface segregation principles
    public void GetPayment()
    {
        throw new NotImplementedException();
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