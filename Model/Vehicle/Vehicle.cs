namespace TransportationLab2.Vehicle;

public class Vehicle(string carBrand, int id)
{
    public Client.Client? Client { get; set; }

    public City.City? TargetCity { get; set; } = new();
    public VehicleState State { get; set; } = VehicleState.Waiting;
    public Point CurrentPos { get; set; }
    
    public int Id
    {
        get => id;
    }

    public string BoxInfo() // Используется для отображения ифномрации в отдельном окне (В ListBox)
    {
        return carBrand + " (" + id + ") [" + State + "]";
    }
    public override string ToString()
    {
        return carBrand + " (" + id + ")";
    }
}