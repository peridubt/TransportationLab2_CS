﻿namespace TransportationLab2.Cargo.Unit;

public interface ICargo
{
    CargoType Type { get; } // тип груза
    string Name { get; } // наименование
    int Cost { get; } // цена за доставку
    int Weight { get; } // вес
    
}