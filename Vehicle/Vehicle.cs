using TransportationLab2.Cargo.Unit;
using TransportationLab2.Client;
using Timer = System.Windows.Forms.Timer;

namespace TransportationLab2.Vehicle;

public class Vehicle(
    string carBrand,
    int id)
    : IVehicle
{
    public Queue<IClient>? Clients { get; set; }
    public string CarBrand { get; } = carBrand;
    public int Id { get; } = id;
    public City.City TargetCity { get; set; }
    public VehicleState State { get; set; } = VehicleState.Waiting;
}