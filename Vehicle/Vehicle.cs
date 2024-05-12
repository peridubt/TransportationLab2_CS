using TransportationLab2.Manager;
using Point = TransportationLab2.Manager.Point;

namespace TransportationLab2.Vehicle;

public class Vehicle
{
    private Thread _thread;
    public Queue<Client.Client>? Clients { get; set; } = new();
    public string CarBrand { get; }
    public int Id { get; }
    public City.City TargetCity { get; set; } = new();
    public VehicleState State { get; set; } = VehicleState.Waiting;
    public PictureBox VehicleAvatar = new();
    public System.Drawing.Point CurrentPos { get; set; } = new();

    public Vehicle(string carBrand, int id)
    {
        CarBrand = carBrand;
        Id = id;
        _thread = new Thread(ProcessOrder);
        _thread.Start();
    }

    public void ProcessOrder()
    {
        while (Clients != null && Clients.Count == 0)
            Thread.Sleep(10);
        State = VehicleState.Driving;
        Animation.Move(TargetCity.Coordinates, this);
        Thread.Sleep(1000);
        // TargetCity = Москва -- едем обратно
        // Animation.Move(В Москву);
    }
}