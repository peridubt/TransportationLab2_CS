﻿using TransportationLab2.Cargo.Unit;

namespace TransportationLab2.Client;

public class Client(
    string name,
    string surname,
    City.City city)
{
    public string Name { get; } = name;
    public string Surname { get; } = surname;
    public City.City City { get; } = city;
    public ICargo? Order { get; set; }
    public ClientState State { get; set; } = ClientState.Inactive;
}