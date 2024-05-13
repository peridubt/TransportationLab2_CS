﻿using TransportationLab2.Cargo.Factory;
using TransportationLab2.Cargo.Unit;
using TransportationLab2.Client;
using TransportationLab2.Vehicle;

namespace TransportationLab2.Manager;

public class Manager
{
    private readonly List<Client.Client?> _clients = new();
    private readonly List<Vehicle.Vehicle> _vehicles = new();
    private readonly List<City.City?> _cities;
    private readonly List<ICargo> _items = new();
    private readonly object _lock = new();

    private void CreateTrucks()
    {
        string[] brands = ["Toyota", "Volvo", "Renault", "MAN"];
        foreach (var brand in brands)
        {
            Vehicle.Vehicle truck = new(brand, _vehicles.Count());
            _vehicles.Add(truck);
        }
    }

    private void AssignOrderToVehicle(int clientChoice)
    {
        int truckChoice = new Random().Next(0, _vehicles.Count);
        _vehicles[truckChoice].TargetCity = _clients[clientChoice]?.City;
        _vehicles[truckChoice].Clients?.Enqueue(_clients[clientChoice]);
    }

    public Manager()
    {
        _cities =
            new List<City.City?>
            {
                new City.City("Volgograd", 968),
                new City.City("Saint Petersburg", 635),
                new City.City("Kazan", 819),
                new City.City("Samara", 968)
            };
        CreateTrucks();
        RestockWarehouse();
    }

    public void RestockWarehouse()
    {
        int range = new Random().Next(0, 4);
        for (int i = 0; i < range; ++i)
            _items.Add(new DangerousFactory("Опасный",
                new Random().Next(100, 500), _items.Count).CreateCargo());
        range = new Random().Next(0, 3);
        for (int i = 0; i < range; ++i)
            _items.Add(new FragileFactory("Хрупкий",
                new Random().Next(200, 600), _items.Count).CreateCargo());
        range = new Random().Next(0, 4);
        for (int i = 0; i < range; ++i)
            _items.Add(new LiquidFactory("Жидкий",
                new Random().Next(400, 900), _items.Count).CreateCargo());
        range = new Random().Next(0, 3);
        for (int i = 0; i < range; ++i)
            _items.Add(new PerishableFactory("Скоропортящийся",
                new Random().Next(300, 900), _items.Count).CreateCargo());
    }

    public void CreateClient()
    {
        if (_clients.Count == 8)
            throw new ManagerException("Client limit has been succeeded (8 clients)");
        string[] surnames = ["Smith", "Jenkins", "Davis", "Johnson"];
        string[] names = ["John", "Jane", "Sally", "Jack"];
        int getSurname = new Random().Next(0, surnames.Length);
        int getName = new Random().Next(0, names.Length);
        int getCity = new Random().Next(0, _cities.Count);
        _clients.Add(new Client.Client(names[getName],
            surnames[getSurname],
            _cities[getCity]));
    }

    public void CreateOrder()
    {
        lock (_lock)
        {
            if (_items.Count == 0)
                throw new ManagerException("Warehouse is empty and needs restock");
            if (_clients.Count == 0)
                throw new ManagerException("No clients");
            int itemChoice = new Random().Next(0, _items.Count);
            int clientChoice = new Random().Next(0, _clients.Count);
            _clients[clientChoice].Order = _items[itemChoice];
            _clients[clientChoice].State = ClientState.WaitingForOrder;

            _items.RemoveAt(itemChoice);
            AssignOrderToVehicle(clientChoice);
        }
    }
}