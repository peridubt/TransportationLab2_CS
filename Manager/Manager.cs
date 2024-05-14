using TransportationLab2.Cargo.Factory;
using TransportationLab2.Cargo.Unit;
using TransportationLab2.Client;

namespace TransportationLab2.Manager;

public class Manager
{
    private readonly List<Client.Client?> _clients = new();
    private readonly List<Vehicle.Vehicle> _vehicles = new();
    private readonly List<City.City?> _cities;
    private readonly List<ICargo> _items = new();
    private readonly List<Thread> _threads = new();
    private readonly object _lock = new();

    // TODO: организовать потоки до конца (возможно, пул потоков)
    // TODO: посмотреть, будет ли работать модель с массивом/списком потоков
    // TODO: сделать графическую часть, протестировать
    // TODO: сделать добавление грузовика пользователем, а не автоматическое создание

    private void AssignOrderToVehicle(int clientChoice)
    {
        int truckChoice = new Random().Next(0, _vehicles.Count);
        _vehicles[truckChoice].TargetCity = _clients[clientChoice]?.City;
        _vehicles[truckChoice].Clients?.Enqueue(_clients[clientChoice]);
        //_threads[truckChoice].Start();
    }
    
    private void CargoInit(ICargoFactory factory)
    {
        int range = new Random().Next(0, 5);
        for (int i = 0; i < range; ++i)
        {
            _items.Add(factory.CreateCargo());
        }
    }

    public Manager()
    {
        _cities =
            new List<City.City?>
            {
                new City.City("Volgograd", 968),
                new City.City("Saint Petersburg", 635),
                new City.City("Kazan", 819),
                new City.City("Samara", 1065)
            };
        RestockWarehouse();
    }

    public void RestockWarehouse()
    {
        CargoInit(new DangerousFactory("Dangerous", new Random().Next(100, 500), _items.Count));
        CargoInit(new FragileFactory("Fragile", new Random().Next(200, 600), _items.Count));
        CargoInit(new LiquidFactory("Liquid", new Random().Next(400, 900), _items.Count));
        CargoInit(new PerishableFactory("Perishable", new Random().Next(300, 900), _items.Count));
    }

    public void CreateVehicle()
    {
        lock (_lock)
        {
            string[] brands = ["Toyota", "Volvo", "Renault", "MAN"];
            var brandChoice = new Random().Next(0, brands.Length);
            var truck = new Vehicle.Vehicle(brands[brandChoice], _vehicles.Count);
            _vehicles.Add(truck);
            // var thread = new Thread(truck.ProcessOrders);
            // _threads.Add(thread);
        }
    }
    
    public void CreateClient()
    {
        if (_clients.Count == 8)
            throw new ManagerException("Client limit has been succeeded (8 clients)");
        string[] surnames = ["Smith", "Jenkins", "Davis", "Johnson"];
        string[] names = ["John", "Jane", "Sally", "Jack"];
        int getSurname = new Random().Next(0, surnames.Length);
        int getName = new Random().Next(0, names.Length);
        int getCity = new Random().Next(0, _cities.Count);
        _clients.Add(new Client.Client(names[getName],
            surnames[getSurname],
            _cities[getCity]));
    }
    
    public void CreateOrder()
    {
        lock (_lock)
        {
            if (_items.Count == 0)
                throw new ManagerException("Warehouse is empty and needs restock");
            if (_clients.Count == 0)
                throw new ManagerException("No clients");
            if (_vehicles.Count == 0)
                throw new ManagerException("No vehicles");
            int itemChoice = new Random().Next(0, _items.Count);
            int clientChoice = new Random().Next(0, _clients.Count);
            _clients[clientChoice].Order = _items[itemChoice];
            _clients[clientChoice].State = ClientState.WaitingForOrder;

            _items.RemoveAt(itemChoice);
            AssignOrderToVehicle(clientChoice);
        }
    }
}