namespace TransportationLab2.Road;

public class Road(City.City cityB, City.City cityA, int length)
{
    public City.City CityA => cityA;
    public City.City CityB => cityB;
    public int Length => length;
}