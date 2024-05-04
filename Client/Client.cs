using TransportationLab2.Cargo.Unit;

namespace TransportationLab2.Client;

public class Client : IClient
{
    public string Name { get; }
    public string Surname { get; }
    public City.City City { get; }
    public ICargo Order { get; }
    public void GetOrder()
    {
        throw new NotImplementedException();
    }
}