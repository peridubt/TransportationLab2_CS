using TransportationLab2.Cargo.Unit;
using TransportationLab2.Client;

namespace TransportationLab2.Vehicle;

public class Vehicle(string carBrand, 
    int loadCapacity, 
    int id, 
    City.City startCity, 
    City.City targetCity)
    : IVehicle
{
    private event Action<IClient>? _сlientServed;
    
    public string CarBrand { get; } = carBrand;
    public int LoadCapacity { get; } = loadCapacity;
    public int Id { get; } = id;
    public List<ICargo> CargoList { get; set; }
    public City.City StartCity { get; set; } = startCity;
    public City.City TargetCity { get; set; } = targetCity;
}