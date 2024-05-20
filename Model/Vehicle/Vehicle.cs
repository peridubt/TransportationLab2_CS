namespace TransportationLab2.Vehicle;

public class Vehicle(string carBrand, int id)
{
    public Client.Client? Client { get; set; }

    public City.City? TargetCity { get; set; } = new();
    public VehicleState State { get; set; } = VehicleState.Waiting;
    public PictureBox VehicleAvatar = new(); // Картинка для грузовика
    public Point CurrentPos { get; set; }


    public string ViewInfo() // Используется для отображения информации в ListView
    {
        return carBrand + " (" + id + ")";
    }

    public string BoxInfo() // Используется для отображения ифномрации в отдельном окне (В ListBox)
    {
        return carBrand + " (" + id + ") [" + State + "]";
    }
}