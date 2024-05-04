﻿namespace TransportationLab2.Cargo.Unit;

public class Liquid(string name, int cost, int weight, int id) : ICargo
{
    public int Id { get; } = id;
    public CargoType Type { get; } = CargoType.Liquid;

    public string Name => name;
    public int Cost => cost;
    public int Weight => weight;
}