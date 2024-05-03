namespace TransportationLab2.Cargo.Unit;

public class Dangerous(string name, int cost, int weight) : ICargo
{
    public CargoType Type { get; } = CargoType.Dangerous;

    public string Name => name;
    public int Cost => cost;
    public int Weight => weight;
}