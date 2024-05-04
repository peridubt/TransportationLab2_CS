using TransportationLab2.Cargo.Unit;
using TransportationLab2.Client;

namespace TransportationLab2.Vehicle;

public interface IVehicle
{
    string CarBrand { get; }
    int LoadCapacity { get; }
    int Id { get; }
    List<ICargo>? CargoList { get; set; }
    City.City StartCity { get; set; }
    City.City TargetCity { get; set; }
    void ReserveQueue(IClient client);  // Добавить клиента в список дел
    void Deliver(int id);  // Доставка груза
    void DelFromQueue(IClient client); // Удаление обслуженного клиента
}