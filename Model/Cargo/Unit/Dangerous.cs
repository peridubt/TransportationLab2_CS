﻿namespace TransportationLab2.Cargo.Unit;

public class Dangerous(string name, int cost,int id) : ICargo
{
    public int Id { get; set; } = id;
    public CargoType Type { get; } = CargoType.Dangerous;
    public string Name => name;
    public int Cost => cost;

    public override string ToString()
    {
        return $"\"{Name}\", {Cost}k RUB.";
    }
}