using TransportationLab2.Cargo.Unit;

namespace TransportationLab2.Client;

public class Client(string name, string surname, City.City city, ICargo order)
    : IClient
{
    public string Name { get; } = name;
    public string Surname { get; } = surname;
    public City.City City { get; } = city;
    public ICargo Order { get; set; } = order;

    public void GetOrder()
    {
        throw new NotImplementedException();
    }
}