public class NewFreeParking : IParking
{
    private readonly Dictionary<int, Car> _parkingLots = new();
    public int AvailableSlots {get; private set;} = 100;
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