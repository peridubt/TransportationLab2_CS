using TransportationLab2.Cargo.Unit;

namespace TransportationLab2.Cargo.Factory;

public class PerishableFactory(string name, int cost, int weight, int id) : ICargoFactory
{
    public ICargo CreateCargo()
    {
        Perishable perishableCargo = new Perishable(name, cost, weight, id);
        return perishableCargo;
    }
}