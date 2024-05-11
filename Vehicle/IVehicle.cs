using System.Diagnostics;
using TransportationLab2.Client;

namespace TransportationLab2.Vehicle;

public interface IVehicle
{
    string CarBrand { get; }
    int Id { get; }
    City.City TargetCity { get; set; }
    Queue<IClient>? Clients { get; set; }
    VehicleState State { get; set; }
}