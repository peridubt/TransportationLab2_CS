namespace TransportationLab2.Vehicle;

public class Vehicle
{
 
    public Client.Client? Client { get; set; }
    public  string CarBrand { get; }
    public int Id { get; }
    public City.City? TargetCity { get; set; } = new();
    public VehicleState State { get; set; } = VehicleState.Waiting;
    public PictureBox VehicleAvatar = new();
    public Point CurrentPos { get; set; } = new();


    public Vehicle(string carBrand, int id)
    {
        CarBrand = carBrand;
        Id = id;
    }

}