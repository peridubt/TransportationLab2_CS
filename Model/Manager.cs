using System.Diagnostics;
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
    private readonly List<City.City?> _cities; // Список городов
    private readonly List<ICargo> _items = []; // Склад
    private readonly List<Thread> _threads = []; // Потоки, которые выполняют логику работы машин
    private readonly object _lock = new(); // Объект для синхронизации потоков (для lock() )
    private readonly Timer _timer = new(); // Таймер, который каждые 5 секунд создаёт новые заказы
    private bool _started; // Индикатор того, начался ли процесс раздачи заказов

    private event Action? NotifyClient; // Event, на который подписывается клиент. (Вот и шаблон "Наблюдатель")
    // При срабатывании данного события клиент получит заказ, а после чего
    // он отпишется от уведомлений по заказу.

    public List<Client.Client> Clients { get; } = [];

    public List<Vehicle.Vehicle> Vehicles { get; } = [];

    public Queue<Vehicle.Vehicle> ActiveVehicles { get; } = new();

    private void OnNotifyClient()
    {
        NotifyClient?.Invoke();
    }

    // Выдача случайного заказа случайному грузовику
    private void AssignOrderToVehicle(int clientChoice)
    {
        int truckChoice = new Random().Next(0, Vehicles.Count);
        var vehicle = Vehicles[truckChoice]; // Выбираем рандомно грузовик

        ActiveVehicles.Enqueue(vehicle); // Ставим его в очередь активных
        Vehicles.RemoveAt(truckChoice); // Убираем из списка тех, кто находится на базе

        vehicle.TargetCity = Clients[clientChoice].City; // Назначаем грузовику город
        vehicle.Client = Clients[clientChoice]; // Назначаем грузовику клиента, которому выдаётся заказ
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
        if (truck.Client != null) truck.Client.State = ClientState.WaitingForOrder; // Состояние клиента - ожидание
        lock (_lock)
        {
            Debug.Assert(truck.TargetCity != null, "truck.TargetCity != null");
            Debug.Assert(client.Order != null, "client.Order != null");
            Animation.MessageHandler.AppendText($"Truck {truck} is driving to {truck.TargetCity.Name}; " +
                                                $"Client: {client}, Order: {client.Order}\r\n");
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
        if (truck.Client != null)
        {
            truck.Client.State = ClientState.RecievingOrder;
            lock (_lock)
            {
                Animation.MessageHandler.AppendText($"{client} is receiving their order "
                                                    + $"in {client.City?.Name}\r\n");
            }

            // Ставим время для выгрузки - 5 с
            Thread.Sleep(5000);
            // Уведомляем клиента о доставке грузка
            OnNotifyClient();
            // Клиент теперь неактивен, а грузовик не имеет текущего заказа
            truck.Client.State = ClientState.Inactive;
        }

        truck.Client = null;
        // Отписываем клиента от уведомлений
        NotifyClient -= client.RecieveOrder;
    }

    // Метод передвижения грузовика в стартовую позицию
    private void DriveBack(Point prevPoint, Vehicle.Vehicle truck)
    {
        lock (_lock)
        {
            Animation.MessageHandler.AppendText($"{truck} is driving "
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
                    Vehicles.Add(ActiveVehicles.Dequeue());
                }
            }
        }
    }

    public Manager()
    {
        // Создаём города
        _cities =
            new List<City.City?>
            {
                new("Volgograd", 1065, Animation.CitiesAvatars["vlg"].Location),
                new("Saint Petersburg", 635, Animation.CitiesAvatars["spb"].Location),
                new("Kazan", 819, Animation.CitiesAvatars["kzn"].Location),
                new("Samara", 968, Animation.CitiesAvatars["smr"].Location)
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
    }

    public void Start()
    {
        lock (_lock)
        {
            if (_started)
                throw new ManagerException("The process is already running!");
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
                throw new ManagerException("The process has already been stopped!");
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

    // Метод создания машины
    private void CreateVehicle()
    {
        lock (_lock) // Синхронизация (общий ресурс - список грузовиков)
        {
            string[] brands = ["Toyota", "Volvo", "Renault", "MAN"];
            var brandChoice = new Random().Next(0, brands.Length); // Задаём случайную марку автомобиля
            var truck = new Vehicle.Vehicle(brands[brandChoice], Vehicles.Count);

            Animation.CreateVehicleImage(); // Создаём картинку для грузовика
            truck.CurrentPos = Animation.VehicleAvatars[truck.Id].Location; // Задаём начальную позицию грузовика
            // (Это будут координаты Москвы на винформе)

            Vehicles.Add(truck); // Добавляем грузовик в общий список

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

            Clients.Add(new Client.Client(names[getName],
                surnames[getSurname],
                _cities[getCity]));
        }
    }

    // Метод создания заказа
    private void CreateOrder(object? obj, EventArgs e)
    {
        lock (_lock)
        {
            if (Vehicles.Count == 0 && ActiveVehicles.Count != 0)
                return;
            if (_items.Count == 0)
                RestockWarehouse();
            // Выбираем случайный предмет со склада и случайного клиента
            var itemChoice = new Random().Next(0, _items.Count);
            var clientChoice = new Random().Next(0, Clients.Count);

            // Назначаем заказ клиенту и ставим его в ожидание
            Clients[clientChoice].Order = _items[itemChoice];
            Clients[clientChoice].State = ClientState.WaitingForOrder;

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