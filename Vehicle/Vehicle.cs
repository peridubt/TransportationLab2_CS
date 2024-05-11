namespace TransportationLab2.Vehicle;

public class Vehicle(
    string carBrand,
    int id)
{
    public Queue<Client.Client>? Clients { get; set; }
    public string CarBrand { get; } = carBrand;
    public int Id { get; } = id;
    public City.City TargetCity { get; set; }
    public VehicleState State { get; set; } = VehicleState.Waiting;
    // public event Action<IClient> NotifyClient;
}