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
    private event Action<IClient>? _notifyClient; // Event, на который подписывается клиент.
    // При срабатывании данного события клиент получит заказ, а после чего
    // отпишется от уведомлений по заказу.

    private Thread _thread; // Поток, который реализует доставку с помощью нескольких грузовиков
    
    public string CarBrand { get; } = carBrand;
    public int LoadCapacity { get; } = loadCapacity;
    public int Id { get; } = id;
    public List<ICargo>? CargoList { get; set; }
    public City.City StartCity { get; set; } = startCity;
    public City.City TargetCity { get; set; } = targetCity;

    public void ReserveQueue(IClient client)
    {
        throw new NotImplementedException();
    }

    public void Deliver(int id)
    {
        throw new NotImplementedException();
    }

    public void DelFromQueue(IClient client)
    {
        throw new NotImplementedException();
    }
}