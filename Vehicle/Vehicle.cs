using TransportationLab2.Cargo.Unit;
using TransportationLab2.Client;
using Timer = System.Windows.Forms.Timer;

namespace TransportationLab2.Vehicle;

public class Vehicle(
    string carBrand,
    int id,
    City.City targetCity)
    : IVehicle
{
    private event Action? _notifyClient; // Event, на который подписывается клиент.

    // При срабатывании данного события клиент получит заказ, а после чего
    // отпишется от уведомлений по заказу.
    private Thread? _thread; // Поток, который реализует доставку с помощью нескольких грузовиков
    private object _lock = new object();
    private Timer _timer = new Timer();
    
    public bool WaitForOrder { get; set; }
    private Queue<IClient>? Clients { get; }
    public string CarBrand { get; } = carBrand;
    public int Id { get; } = id;
    public City.City TargetCity { get; set; } = targetCity;

    private event Action NotifyClient
    {
        add { _notifyClient += value; }
        remove { _notifyClient -= value; }
    }

    public void GetRandomOrder(List<IClient> clientsOrderList)
    {
        var index = new Random().Next(0, clientsOrderList.Count);
        Clients?.Enqueue(clientsOrderList[index]);
        clientsOrderList.RemoveAt(index);
    }

    public void Start()
    {
        _thread = new Thread(VehicleLogic);
        _thread.Start();
    }

    private void DriveToClient()
    {
        
    }
    
    private void CheckForOrders(List<IClient> clientsOrderList)
    {
        if (WaitForOrder)
            return;
        try
        {
            _timer.Stop();
            if (clientsOrderList.Count != 0)
                GetRandomOrder(clientsOrderList);
        }
        finally
        {
            _timer.Start();
        }
    }

    private void VehicleLogic()
    {
        
    }

    public void ReserveQueue(IClient client)
    {
        lock (_lock)
        {
            Clients?.Enqueue(client);
            NotifyClient += client.GetOrder;
        }
    }

    public void Deliver()
    {
        _notifyClient?.Invoke();
    }

    public void DelFromQueue()
    {
        lock (_lock)
        {
            var client = Clients?.Dequeue();
            if (client != null)
                NotifyClient -= client.GetOrder;
        }
    }
}