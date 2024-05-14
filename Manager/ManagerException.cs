namespace TransportationLab2;

public class ManagerException: Exception
{
    public ManagerException() {}
    public ManagerException(string message) : base(message) { }
}

public class NoClientsException : ManagerException
{
    public NoClientsException() {}
    public NoClientsException(string message) : base(message) { }
}

public class NoVehiclesException : ManagerException
{
    public NoVehiclesException() {}
    public NoVehiclesException(string message) : base(message) { }
}

public class EmptyWarehouseException : ManagerException
{
    public  EmptyWarehouseException() {}
    public  EmptyWarehouseException(string message) : base(message) { }
}