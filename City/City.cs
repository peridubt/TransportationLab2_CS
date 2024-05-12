﻿namespace TransportationLab2.City;

public class City
{
    public string Name { get; }
    public int RoadLength { get; }
    public System.Drawing.Point Coordinates { get; }
    
    
    public City(string name = "", int roadLength = 0, System.Drawing.Point coordinates = new())
    {
        Name = name;
        RoadLength = roadLength;
        Coordinates = coordinates;
    }
}