namespace TransportationLab2.Manager;

static class Animation
{
    private static List<Point> GetCoordsBetween(Point start, Point end, int count)
    {
        List<Point> Coords = new List<Point>();

        double distanceX = end.X - start.X;
        double distanceY = end.Y - start.Y;
        double stepX = distanceX / (count + 1);
        double stepY = distanceY / (count + 1);

        for (int j = 1; j <= count; j++)
        {
            double x = start.X + j * stepX;
            double y = start.Y + j * stepY;
            Coords.Add(new Point(x, y));
        }
        Coords.Add(end);
        return Coords;
    }
}