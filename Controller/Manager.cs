using System.Text;
using TransportationLab2.Cargo.Factory;
using TransportationLab2.Cargo.Unit;
using TransportationLab2.Client;
using TransportationLab2.Vehicle;

namespace TransportationLab2.Controller;

public class Manager
{
    private readonly List<Client.Client?> _clients = new();
    private readonly List<Vehicle.Vehicle> _vehicles = new();
    private readonly List<City.City?> _cities;
    private readonly List<ICargo> _items = new();
    private readonly List<Thread> _threads = new();
    private readonly object _lock = new();
    private ListView vehiclesView;
    private ListView clientsView;
    private Queue<Vehicle.Vehicle> _activeVehicles = new();
    private event Action? NotifyClient; // Event, на который подписывается клиент.
    // При срабатывании данного события клиент получит заказ, а после чего
    // отпишется от уведомлений по заказу.
    private void OnNotifyClient()
    {
        NotifyClient?.Invoke();
    }

    // TODO: организовать потоки до конца (возможно, пул потоков)
    // TODO: сделать графическую часть, протестировать

    private void AssignOrderToVehicle(int clientChoice)
    {
        int truckChoice = new Random().Next(0, _vehicles.Count);
        var vehicle = _vehicles[truckChoice];
        _activeVehicles.Enqueue(vehicle);
        _vehicles.RemoveAt(truckChoice);
        vehicle.TargetCity = _clients[clientChoice]?.City;
        vehicle.Client = _clients[clientChoice];
        vehicle.VehicleAvatar.Visible = true;
    }

    private void CargoInit(ICargoFactory factory)
    {
        int range = new Random().Next(0, 5);
        for (int i = 0; i < range; ++i)
        {
            _items.Add(factory.CreateCargo());
        }
    }

    private void DriveToClient(Client.Client client, Vehicle.Vehicle truck)
    {
        NotifyClient += client.RecieveOrder;
        truck.State = VehicleState.Driving;
        truck.Client.State = ClientState.WaitingForOrder;
        Animation.Move(truck.TargetCity.Coordinates, ref truck);
    }

    private void Offload(Client.Client client, Vehicle.Vehicle truck)
    {
        truck.State = VehicleState.Offloading;
        truck.Client.State = ClientState.RecievingOrder;
        Thread.Sleep(1000);
        OnNotifyClient();
        truck.Client.State = ClientState.Inactive;
        truck.Client = null;
        NotifyClient -= client.RecieveOrder;
    }

    private void DriveBack(Point prevPoint, Vehicle.Vehicle truck)
    {
        Animation.Move(prevPoint, ref truck);
        truck.State = VehicleState.Waiting;
        truck.VehicleAvatar.Visible = false;
    }

    private void ProcessOrders(Vehicle.Vehicle truck)
    {
        while (true)
        {
            while (truck.Client == null)
                Thread.Sleep(100);
            // как только грузовик получил заказ, он начинает своё движение
            var client = truck.Client;
            if (client != null)
            {
                var prevPoint = truck.CurrentPos;
                DriveToClient(client, truck);
                Offload(client, truck);
                DriveBack(prevPoint, truck);
                lock (_lock)
                {
                    _vehicles.Add(_activeVehicles.Dequeue());
                }
            }
        }
    }

    public Manager(ref ListView clientsView, ref ListView vehiclesView)
    {
        this.vehiclesView = vehiclesView;
        this.clientsView = clientsView;
        _cities =
            new List<City.City?>
            {
                new("Volgograd", 1065, new(797, 798)),
                new("Saint Petersburg", 635, new(338, 109)),
                new("Kazan", 819, new(910, 354)),
                new("Samara", 968, new(971, 511))
            };
        RestockWarehouse();
    }

    public void RestockWarehouse()
    {
        lock (_lock)
        {
            CargoInit(new DangerousFactory("Dangerous", new Random().Next(100, 500), 0));
            CargoInit(new FragileFactory("Fragile", new Random().Next(200, 600), 0));
            CargoInit(new LiquidFactory("Liquid", new Random().Next(400, 900), 0));
            CargoInit(new PerishableFactory("Perishable", new Random().Next(300, 900), 0));
            for (int i = 0; i < _items.Count; ++i)
            {
                _items[i].Id = i;
            }
        }
    }

    public void CreateVehicle(ref PictureBox vehcilePBox)
    {
        lock (_lock)
        {
            if (_vehicles.Count + _activeVehicles.Count == 4)
                throw new ManagerException("Vehicle limit has been succeeded (4 vehicles)");
            string[] brands = ["Toyota", "Volvo", "Renault", "MAN"];
            var brandChoice = new Random().Next(0, brands.Length);
            var truck = new Vehicle.Vehicle(brands[brandChoice], _vehicles.Count);
            Animation.CreateVehicleImage(ref truck);
            vehcilePBox = truck.VehicleAvatar;
            _vehicles.Add(truck);
            var thread = new Thread(() => ProcessOrders(truck));
            vehiclesView.Items.Add(truck.CarBrand);
            _threads.Add(thread);
            _threads.Last().Start();
        }
    }

    public void CreateClient()
    {
        lock (_lock)
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
            clientsView.Items.Add(names[getName] + " " + surnames[getSurname]);
        }
    }

    public void CreateOrder()
    {
        lock (_lock)
        {
            if (_items.Count == 0)
                throw new EmptyWarehouseException("Warehouse is empty and needs restock");
            if (_clients.Count == 0)
                throw new NoClientsException("No clients");
            if (_vehicles.Count == 0 && _activeVehicles.Count != 0)
                throw new ManagerException("No avaliable vehicles");
            if (_vehicles.Count == 0)
                throw new NoVehiclesException("No vehicles");
            int itemChoice = new Random().Next(0, _items.Count);
            int clientChoice = new Random().Next(0, _clients.Count);
            _clients[clientChoice].Order = _items[itemChoice];
            _clients[clientChoice].State = ClientState.WaitingForOrder;
            _items.RemoveAt(itemChoice);
            AssignOrderToVehicle(clientChoice);
        }
    }

    public string ViewWarehouse()
    {
        lock (_lock)
        {
            var result = new StringBuilder();
            result.AppendLine($"ID  Name                     Type                    Cost   ");
            foreach (var item in _items)
            {
                result.AppendLine($"{item.Id,-4}\"{item.Name,-20}\"" +
                    $"{item.Type.ToString(),-20}{item.Cost,-5}");
            }
            return result.ToString();
        }
    }
}