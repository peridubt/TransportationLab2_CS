using System.Text;
using TransportationLab2.Cargo.Factory;
using TransportationLab2.Cargo.Unit;
using TransportationLab2.Client;
using TransportationLab2.Vehicle;

namespace TransportationLab2.Controller;

public class Manager
{
    private const int _maxClients = 10; // Максимальное количество клиентов 
    private const int _maxVehicles = 5; // Макс. кол-во машин
    private readonly List<Client.Client?> _clients = new(); // Список клиентов
    private readonly List<Vehicle.Vehicle> _vehicles = new(); // Список машин
    private readonly List<City.City?> _cities; // Список городов
    private readonly List<ICargo> _items = new(); // Склад
    private readonly List<Thread> _threads = new(); // Потоки, которые выполняют логику работы машин
    private readonly object _lock = new(); // Объект для синхронизации потоков (для lock() )
    private ListView vehiclesView; // Вывод машин в основную форму
    private ListView clientsView; // Вывод клиентов в основную форму
    private Queue<Vehicle.Vehicle> _activeVehicles = new(); // очередь грузовиков, находящихся в пути
    private event Action? NotifyClient; // Event, на который подписывается клиент. (Вот и шаблон "Наблюдатель")
    // При срабатывании данного события клиент получит заказ, а после чего
    // он отпишется от уведомлений по заказу.

    public List<Client.Client> Clients { get=>_clients; }
    public List<Vehicle.Vehicle> Vehicles { get => _vehicles; } 
    public Queue<Vehicle.Vehicle> ActiveVehicles { get => _activeVehicles; } 
    private void OnNotifyClient()
    {
        NotifyClient?.Invoke();
    }

    // Выдача случайного заказа случайному грузовику
    private void AssignOrderToVehicle(int clientChoice) 
    {
        int truckChoice = new Random().Next(0, _vehicles.Count);
        var vehicle = _vehicles[truckChoice]; // Выбираем рандомно грузовик

        _activeVehicles.Enqueue(vehicle); // Ставим его в очередь активных
        _vehicles.RemoveAt(truckChoice); // Убираем из списка тех, кто находится на базе

        vehicle.TargetCity = _clients[clientChoice]?.City; // Назначаем грузовику город
        vehicle.Client = _clients[clientChoice]; // Назначаем грузовику клиента, которому выдаётся заказ
        vehicle.VehicleAvatar.Visible = true; // Делаем изображение грузовика видимым для пользователя
    }

    // Инициализация отдельного типа груза через фабрику
    private void CargoInit(ICargoFactory factory)
    {
        int range = new Random().Next(0, 5);
        for (int i = 0; i < range; ++i)
        {
            _items.Add(factory.CreateCargo());
        }
    }

    // Метод передвижения грузовика от базы в город клиента
    private void DriveToClient(Client.Client client, Vehicle.Vehicle truck)
    {
        NotifyClient += client.RecieveOrder; // Клиент подписывается на событие, которое 
        truck.State = VehicleState.Driving; // Состояние грузовика меняется на "В пути"
        truck.Client.State = ClientState.WaitingForOrder; // Состояние клиента - ожидание
        Animation.Move(truck.TargetCity.Coordinates, ref truck, false); // Вызывается метод, который перемещает
        // PictureBox грузовика из начальной точки в указанную
    }

    // Выгрузка товара
    private void Offload(Client.Client client, Vehicle.Vehicle truck) 
    {
        // Меняем состояния для клиена и грузовика
        truck.State = VehicleState.Offloading;
        truck.Client.State = ClientState.RecievingOrder;
        // Ставим время для выгрузки - 5 с
        Thread.Sleep(5000);
        // Уведомляем клиента о доставке грузка
        OnNotifyClient();
        // Клиент теперь неактивен, а грузовик не имеет текущего заказа
        truck.Client.State = ClientState.Inactive;
        truck.Client = null;
        // Отписываем клиента от уведомлений
        NotifyClient -= client.RecieveOrder;
    }
 
    // Метод передвижения грузовика в стартовую позицию
    private void DriveBack(Point prevPoint, Vehicle.Vehicle truck)
    {
        truck.State = VehicleState.Driving;
        Animation.Move(prevPoint, ref truck, true);
        truck.State = VehicleState.Waiting;
        truck.VehicleAvatar.Visible = false;
    }

    // Метод, который делегирует заказом, который назначен одному грузовику
    private void ProcessOrders(Vehicle.Vehicle truck)
    {
        while (true) // Начинается бесконечный цикл
        {
            while (truck.Client == null)
                // Каждые 100 мс происходит запрос: не получил ли грузовик новый заказ 
                Thread.Sleep(100); 
            
            // Как только грузовик получил заказ, он начинает своё движение
            var client = truck.Client;
            if (client != null)
            {
                var prevPoint = truck.CurrentPos;
                DriveToClient(client, truck);
                Offload(client, truck);
                DriveBack(prevPoint, truck);
                lock (_lock)
                {
                    // После завершения заказа грузовик выходит из очереди активных машин
                    // И возвращается в общий список грузовиков
                    _vehicles.Add(_activeVehicles.Dequeue());
                }
            }
        }
    }

    public Manager(ref ListView clientsView, ref ListView vehiclesView)
    {
        // В начале менеджеру назначаем отображение клиентов и грузовиков
        this.vehiclesView = vehiclesView;
        this.clientsView = clientsView;
        
        // Создаём города
        _cities =
            new List<City.City?>
            {
                new("Volgograd", 1065, new(811, 837)),
                new("Saint Petersburg", 635, new(360, 163)),
                new("Kazan", 819, new(929, 408)),
                new("Samara", 968, new(998, 551))
            };
        
        // Наполняем склад
        RestockWarehouse();
    }

    // Метод наполнения склада различными грузами
    public void RestockWarehouse()
    {
        lock (_lock) // Синхронизация потоков (т.к. склад - это общий ресурс, который задействуется потоками)
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

    // Метод создания одного клиента
    public void CreateVehicle(ref PictureBox vehcilePBox)
    {
        lock (_lock) // Синхронизация (общий ресурс - список грузовиков)
        {
            if (_vehicles.Count + _activeVehicles.Count == _maxVehicles)
                throw new ManagerException($"Vehicle limit has been succeeded ({_maxVehicles} vehicles)");
            string[] brands = ["Toyota", "Volvo", "Renault", "MAN"];
            var brandChoice = new Random().Next(0, brands.Length); // Задаём случайную марку автомобиля
            var truck = new Vehicle.Vehicle(brands[brandChoice], _vehicles.Count);

            Animation.CreateVehicleImage(ref truck); // Создаём картинку для грузовика
            vehcilePBox = truck.VehicleAvatar; // Для внешней программы задаём PictureBox

            _vehicles.Add(truck); // Добавляем грузовик в общий список
            vehiclesView.Items.Add(truck.ViewInfo()); // Отображаем грузовик во внешней программе

            // Создаём новый поток и добавляем его в список
            var thread = new Thread(() => ProcessOrders(truck)); 
            _threads.Add(thread);
            // Сразу же запускаем его, т.к. процесс грузовика - это бесконечный цикл
            _threads.Last().Start(); 
        }
    }

    // Метод создания клиента
    public void CreateClient()
    {
        lock (_lock)
        {
            if (_clients.Count == _maxClients)
                throw new ManagerException($"Client limit has been succeeded ({_maxClients} clients)");
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

    // Метод создания заказа
    public void CreateOrder()
    {
        lock (_lock)
        {
            if (_items.Count == 0)
                throw new EmptyWarehouseException("Warehouse is empty and needs restock");
            if (_clients.Count == 0)
                throw new NoClientsException("No clients");
            if (_vehicles.Count == 0 && _activeVehicles.Count != 0)
                throw new ManagerException("No available vehicles");
            if (_vehicles.Count == 0)
                throw new NoVehiclesException("No vehicles");

            // Выбираем случайный предмет со склада и случайного клиента
            int itemChoice = new Random().Next(0, _items.Count);
            int clientChoice = new Random().Next(0, _clients.Count);

            // Назначаем заказ клиенту и ставим его в ожидание
            _clients[clientChoice].Order = _items[itemChoice];
            _clients[clientChoice].State = ClientState.WaitingForOrder;

            // Предмет убирается со склада
            _items.RemoveAt(itemChoice);
            AssignOrderToVehicle(clientChoice); // Начинается разначение заказа грузовику
        }
    }

    // Вывод информации о складе
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