namespace TransportationLab2.Manager;

public class Animation
{
    private static List<Point> GetCoordsBetween(Point start, Point end, int count)
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
            points.Add(new Point(x, y));
        }

        points.Add(end);
        return points;
    }
}