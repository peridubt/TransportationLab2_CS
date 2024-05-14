namespace TransportationLab2.Manager;

public static class Animation
{
    private static List<Point> GetCoordsBetween(Point start,
        Point end, int count)
    {
        List<Point> points = [];

        var distanceX = end.X - start.X;
        var distanceY = end.Y - start.Y;
        var stepX = distanceX / (count + 1);
        var stepY = distanceY / (count + 1);

        for (var j = 1; j <= count; j++)
        {
            var x = start.X + j * stepX;
            var y = start.Y + j * stepY;
            points.Add(new(x, y));
        }

        points.Add(end);
        return points;
    }

    public static void Move(Point end, Vehicle.Vehicle truck)
    {
        if (truck.TargetCity != null)
        {
            int count = (truck.TargetCity.RoadLength / 100) * 100;
            var points = GetCoordsBetween(truck.CurrentPos, end, count);
            foreach (var point in points)
            {
                truck.VehicleAvatar.Location = new Point(point.X, point.Y);
                Thread.Sleep(10);
            }
        }
    }
}