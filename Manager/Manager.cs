using TransportationLab2.Cargo;
using TransportationLab2.Vehicle;

namespace TransportationLab2.Manager;

public class Manager
{
    private List<Client.Client> _clients = new();
    private List<Vehicle.Vehicle> _vehicles = new();
    private Dictionary<City.City, int> _roads;
    private Warehouse _warehouse;
    private List<Thread> _threads;
    private object _lock = new();

    // private object _moveLock = new();
    private event Action<Client.Client> _notifyClient; // Event, на который подписывается клиент.
    // При срабатывании данного события клиент получит заказ, а после чего
    // отпишется от уведомлений по заказу.

    private event Action<Client.Client> NotifyClient
    {
        add { _notifyClient += value; }
        remove { _notifyClient -= value; }
    }

    private void CreateRoads()
    {
        _roads = new Dictionary<City.City, int>
        {
            [City.City.Vlg] = 968,
            [City.City.Spb] = 635,
            [City.City.Kzn] = 819,
            [City.City.Smr] = 1065
        };
    }

    private void CreateClients()
    {
        Client.Client client = new("Иванов", "Иван", City.City.Kzn);
        _clients.Add(client);
        client = new("Андреев", "Андрей", City.City.Spb);
        _clients.Add(client);
        client = new("Фролова", "Ольга", City.City.Smr);
        _clients.Add(client);
        client = new("Александрова", "Александра", City.City.Vlg);
        _clients.Add(client);
    }

    private void AssignOrderToVehicle(int clientChoice)
    {
        int truckChoice = new Random().Next(0, _vehicles.Count);
        _vehicles[truckChoice].Clients?.Enqueue(_clients[clientChoice]);
        _vehicles[truckChoice].TargetCity = _clients[clientChoice].City;
        var thread = new Thread(() => ProcessOrder(_vehicles[truckChoice]));
        _threads.Add(thread);
        _threads.Last().Start();
    }

    private void GiveOrderToClient(Client.Client client)
    {
        client.State = Client.ClientState.RecievingOrder;
        client.Order = null;
    }

    // Украдено из чужой лабы:
    /*
        // Метод перемещающий объект по Canvas
        private void MoveTo(Point end)
        {
            lock (_moveLock) // Захватываем данный объект(синхронизация)
            {
                var tempState = State;
                try
                {
                    State = StateEnum.Moving;   // Изменение статуса клиента
                    var points = GetCoordsBetween(_currentCoord, end, 20);
                    foreach (var point in points)
                    {
                        _currentCoord = point;
                        // Так как WPF приложение работает с UI потоком,
                        // то Dispatcher осуществляет синхронизацию UI потока с созданными дополнительно
                        _window.Dispatcher.Invoke(() =>
                        {
                            Canvas.SetLeft(MyImage, _currentCoord.X);
                            Canvas.SetTop(MyImage, _currentCoord.Y);
                        });
                        Thread.Sleep(100);
                    }
                }
                finally
                {
                    State = tempState;
                }
            }
        }
     */
    private void DriveToClient(Vehicle.Vehicle truck)
    {
        truck.State = VehicleState.Driving;
    }

    private void ProcessOrder(Vehicle.Vehicle vehicle)
    {
        vehicle.State = VehicleState.Driving;

        int timeSleep = _roads[vehicle.TargetCity] * 10;
        Thread.Sleep(timeSleep);
    }

    public Manager()
    {
        CreateRoads();
        CreateClients();
        _warehouse = new();
        _threads = new();
    }

    public void RestockWarehouse()
    {
        _warehouse.FillWarehouse();
    }

    public void CreateVehicle()
    {
        if (_vehicles.Count == 4)
            throw new ManagerException("Truck limit has been succeeded (4 vehicles)");
        string[] brands = { "Toyota", "Volvo", "Renault", "MAN" };
        int brandId = new Random().Next(0, brands.Length);
        Vehicle.Vehicle truck = new(brands[brandId], _vehicles.Count + 1);
        _vehicles.Add(truck);
    }

    public void CreateOrder()
    {
        lock (_lock)
        {
            if (_warehouse.Items.Count == 0)
                throw new ManagerException("Warehouse is empty and needs restock");
            if (_vehicles.Count == 0)
                throw new ManagerException("No vehicles");
            if (_clients.Count == 0)
                throw new ManagerException("No clients");
            int itemChoice = new Random().Next(0, _warehouse.Items.Count);
            int clientChoice = new Random().Next(0, _clients.Count);
            _clients[clientChoice].Order = _warehouse.Items[itemChoice];
            _warehouse.Items.RemoveAt(itemChoice);
            NotifyClient += GiveOrderToClient;
            AssignOrderToVehicle(clientChoice);
        }
    }
}