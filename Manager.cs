using TransportationLab2.Cargo;
using TransportationLab2.Client;
using TransportationLab2.Vehicle;

namespace TransportationLab2;

public class Manager
{
    private List<IClient> _clients = new List<IClient>();
    private List<IVehicle> _vehicles = new List<IVehicle>();
    private Dictionary<City.City, int> _roads;
    private Warehouse _warehouse;

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

    public Manager()
    {
        CreateRoads();
        CreateClients();
        _warehouse = new Warehouse();
    }

    public void CreateVehicle()
    {
        if (_vehicles.Count == 4)
            throw new ManagerException("Превышен лимит грузовиков (4 шт.)");
        string[] brands = { "Toyota", "Volvo", "Renault", "MAN" };
        int brandId = new Random().Next(0, brands.Length);
        Vehicle.Vehicle truck = new Vehicle.Vehicle(brands[brandId],
            _vehicles.Count + 1);
        _vehicles.Add(truck);
    }

    public void RestockWarehouse()
    {
        _warehouse.FillWarehouse();
    }
}