namespace TransportationLab2.Vehicle;

public class Vehicle
{
    private readonly string _carBrand;
    private readonly int _id;

    public Client.Client? Client { get; set; }

    public string CarBrand
    {
        get => _carBrand;
    }

    public int Id
    {
        get => _id;
    }

    public City.City? TargetCity { get; set; } = new();
    public VehicleState State { get; set; } = VehicleState.Waiting;
    public PictureBox VehicleAvatar = new();
    public Point CurrentPos { get; set; }


    public Vehicle(string carBrand, int id)
    {
        _carBrand = carBrand;
        _id = id;
    }

    public override string ToString()
    {
        return _carBrand + " (" + _id + ")";
    }

    public string ViewInfo()
    {
        return _carBrand + " (" + _id + ") [" + State + "]";
    }
}