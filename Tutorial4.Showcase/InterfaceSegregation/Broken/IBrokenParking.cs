namespace Tutorial4.Showcase.InterfaceSegregation.Broken
{
    // What if free parking???
    public interface IBrokenParking
    {
        void Park(Car car);
        void Unpark(Car car);
        string GenerateTicket();
        void GetPayment();
    }
}