using System.Diagnostics;
using TransportationLab2.Manager;

namespace TransportationLab2.Vehicle;

public class Vehicle
{
    private Thread _thread;

    private event Action? _notifyClient; // Event, на который подписывается клиент.
    // При срабатывании данного события клиент получит заказ, а после чего
    // отпишется от уведомлений по заказу.
    public Queue<Client.Client?>? Clients { get; } = new();
    public string CarBrand { get; }
    public int Id { get; }
    public City.City? TargetCity { get; set; } = new();
    public VehicleState State { get; set; } = VehicleState.Waiting;
    public PictureBox VehicleAvatar = new();
    public System.Drawing.Point CurrentPos { get; set; } = new();

    private void OnNotifyClient()
    {
        _notifyClient?.Invoke();
    }

    public Vehicle(string carBrand, int id)
    {
        CarBrand = carBrand;
        Id = id;
        _thread = new Thread(ProcessOrders);
        _thread.Start();
    }

    public void ProcessOrders()
    {
        while (true) // заходим в бесконечный цикл
        {
            // в нём грузовик имеет начальное состояние ожидания
            while (Clients != null && Clients.Count == 0)
                Thread.Sleep(10);
            // как только грузовик получил заказ, он начинает своё движение
            var client = Clients?.Peek();
            if (client != null)
            {
                _notifyClient += client.RecieveOrder;
                State = VehicleState.Driving;
                Debug.Assert(TargetCity != null, nameof(TargetCity) + " != null");
                Animation.Move(TargetCity.Coordinates, this);
                Thread.Sleep(1000);
                OnNotifyClient();
                Clients?.Dequeue();
                _notifyClient -= client.RecieveOrder;
            }

            // TargetCity = Москва -- едем обратно
            // Animation.Move(В Москву);
        }
    }
}