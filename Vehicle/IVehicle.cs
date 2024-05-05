using TransportationLab2.Cargo.Unit;
using TransportationLab2.Client;

namespace TransportationLab2.Vehicle;

public interface IVehicle
{
    string CarBrand { get; }
    int Id { get; }
    City.City TargetCity { get; set; }
    void ReserveQueue(IClient client); // Добавить клиента в список дел
    void Deliver(); // Доставка груза
    void DelFromQueue(); // Удаление обслуженного клиента
}