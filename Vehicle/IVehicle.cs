using TransportationLab2.Cargo.Unit;

namespace TransportationLab2.Vehicle;

public interface IVehicle
{
    string CarBrand { get; }
    int LoadCapacity { get; }
    int Id { get; }
    List<ICargo> CargoList { get; set; }
    City.City StartCity { get; set; }
    City.City TargetCity { get; set; }
}