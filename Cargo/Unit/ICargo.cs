namespace TransportationLab2.Cargo.Unit;

public interface ICargo
{
    int Id { get; set; }
    CargoType Type { get; } // тип груза
    string Name { get; } // наименование
    int Cost { get; } // цена за доставку
}