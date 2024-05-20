using System.Text;
using TransportationLab2.Cargo.Factory;
using TransportationLab2.Cargo.Unit;
using TransportationLab2.Client;
using TransportationLab2.Controller;
using TransportationLab2.Vehicle;
using Timer = System.Windows.Forms.Timer;

namespace TransportationLab2.Model;

public class Manager
{
    private const int MaxClients = 10; // Максимальное количество клиентов 
    private const int MaxVehicles = 5; // Макс. кол-во машин
    private readonly List<Client.Client?> _clients = []; // Список клиентов
    private readonly List<Vehicle.Vehicle> _vehicles = []; // Список машин
    private readonly List<City.City?> _cities; // Список городов
    private readonly List<ICargo> _items = []; // Склад
    private readonly List<Thread> _threads = []; // Потоки, которые выполняют логику работы машин
    private readonly object _lock = new(); // Объект для синхронизации потоков (для lock() )
    private readonly Queue<Vehicle.Vehicle> _activeVehicles = new(); // очередь грузовиков, находящихся в пути
    private readonly Timer _timer = new();
    private bool _started = false;

    private event Action? NotifyClient; // Event, на который подписывается клиент. (Вот и шаблон "Наблюдатель")
    // При срабатывании данного события клиент получит заказ, а после чего
    // он отпишется от уведомлений по заказу.

    public List<Client.Client> Clients
    {
        get => _clients;
    }

    public List<Vehicle.Vehicle> Vehicles
    {
        get => _vehicles;
    }

    public Queue<Vehicle.Vehicle> ActiveVehicles
    {
        get => _activeVehicles;
    }

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
        Animation.VehicleAvatars[vehicle.Id].Visible = true; // Делаем изображение грузовика видимым для пользователя
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
        lock (_lock)
        {
            Animation.MessageHandler.AppendText($"Truck {truck.ToString()} is driving to {truck.TargetCity.Name}; " +
                                                $"Client: {client.ToString()}, Order: {client.Order.ToString()}\r\n");
        }

        // Вызывается метод, который перемещает
        // PictureBox грузовика из начальной точки в указанную
        Animation.Move(truck.TargetCity.Coordinates, ref truck, false);
    }

    // Выгрузка товара
    private void Offload(Client.Client client, Vehicle.Vehicle truck)
    {
        // Меняем состояния для клиена и грузовика
        truck.State = VehicleState.Offloading;
        truck.Client.State = ClientState.RecievingOrder;
        lock (_lock)
        {
            Animation.MessageHandler.AppendText($"{client.ToString()} is receiving their order "
                                                + $"in {client.City?.Name}\r\n");
        }

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
        lock (_lock)
        {
            Animation.MessageHandler.AppendText($"{truck.ToString()} is driving "
                                                + $"back to Moscow\r\n");
        }

        truck.State = VehicleState.Driving;
        Animation.Move(prevPoint, ref truck, true);
        truck.State = VehicleState.Waiting;
        Animation.VehicleAvatars[truck.Id].Visible = false;
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

    public Manager(ref List<PictureBox> vehiclePBox, ref TextBox messagesTextBox,
        ref Dictionary<string, PictureBox> cityPBox)
    {
        // В начале менеджеру назначаем отображение клиентов и грузовиков

        // Создаём города
        _cities =
            new List<City.City?>
            {
                new("Volgograd", 1065, cityPBox["vlg"].Location),
                new("Saint Petersburg", 635, cityPBox["spb"].Location),
                new("Kazan", 819, cityPBox["kzn"].Location),
                new("Samara", 968, cityPBox["smr"].Location)
            };

        // Наполняем склад
        RestockWarehouse();
        // Создаём заранее 5 грузовиков и 10 клиентов
        for (var i = 0; i < MaxClients; ++i)
        {
            CreateClient();
            if (i < MaxVehicles)
            {
                CreateVehicle();
            }
        }
        vehiclePBox = Animation.VehicleAvatars;
        Animation.MessageHandler = messagesTextBox;
    }

    public void Start()
    {
        lock (_lock)
        {
            if (_started)
                throw new ManagerException("The process has already started!");
            Animation.MessageHandler.AppendText($"[STARTING IN 5 SECS...]\r\n");
            _started = true;
            _timer.Interval = 5000;
            _timer.Tick += CreateOrder;
            _timer.Start();
        }
    }

    public void Stop()
    {
        lock (_lock)
        {
            if (!_started)
                throw new ManagerException("The process has already stopped!");
            Animation.MessageHandler.AppendText($"[WAITING FOR REMAINING ORDERS TO BE COMPLETE...]\r\n");
            _started = false;
            _timer.Stop();
        }
    }

    // Метод наполнения склада различными грузами
    private void RestockWarehouse()
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
    private void CreateVehicle()
    {
        lock (_lock) // Синхронизация (общий ресурс - список грузовиков)
        {
            string[] brands = ["Toyota", "Volvo", "Renault", "MAN"];
            var brandChoice = new Random().Next(0, brands.Length); // Задаём случайную марку автомобиля
            var truck = new Vehicle.Vehicle(brands[brandChoice], _vehicles.Count);

            Animation.CreateVehicleImage(ref truck); // Создаём картинку для грузовика

            _vehicles.Add(truck); // Добавляем грузовик в общий список

            // Создаём новый поток и добавляем его в список
            var thread = new Thread(() => ProcessOrders(truck));
            _threads.Add(thread);
            // Сразу же запускаем его, т.к. процесс грузовика - это бесконечный цикл
            _threads.Last().Start();
        }
    }

    // Метод создания клиента
    private void CreateClient()
    {
        lock (_lock)
        {
            string[] surnames = ["Smith", "Jenkins", "Davis", "Johnson"];
            string[] names = ["John", "Jane", "Sally", "Jack"];

            int getSurname = new Random().Next(0, surnames.Length);
            int getName = new Random().Next(0, names.Length);
            int getCity = new Random().Next(0, _cities.Count);

            _clients.Add(new Client.Client(names[getName],
                surnames[getSurname],
                _cities[getCity]));
        }
    }

    // Метод создания заказа
    private void CreateOrder(object? obg, EventArgs e)
    {
        lock (_lock)
        {
            if (_vehicles.Count == 0 && _activeVehicles.Count != 0)
                return;
            if (_items.Count == 0)
                RestockWarehouse();
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