namespace TransportationLab2.Client;

public enum ClientState // состояния для клиента
{
    WaitingForOrder, // ждёт заказ
    RecievingOrder, // полуает заказ
    Inactive // не активен (до нового заказа)
}