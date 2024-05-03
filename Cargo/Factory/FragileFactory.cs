using TransportationLab2.Cargo.Unit;

namespace TransportationLab2.Cargo.Factory;

public class FragileFactory(string name, int cost, int weight) : ICargoFactory
{
    public ICargo CreateCargo()
    {
        Fragile dangerousCargo = new Fragile(name, cost, weight);
        return dangerousCargo;
    }
}