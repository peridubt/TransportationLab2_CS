using TransportationLab2.Cargo.Unit;

namespace TransportationLab2.Cargo.Factory;

public class LiquidFactory(string name, int cost, int weight) : ICargoFactory
{
    public ICargo CreateCargo()
    {
        Liquid liquidCargo = new Liquid(name, cost, weight);
        return liquidCargo;
    }
}