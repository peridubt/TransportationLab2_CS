namespace TransportationLab2.Controller;

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

    public static void CreateVehicleImage(ref Vehicle.Vehicle vehicle)
    {
        vehicle.VehicleAvatar.Image = Image.FromFile("C:\\Users\\honor\\Desktop\\Лабы\\CSharp\\" +
            "TransportationLab2\\Resources\\truck.png");
        vehicle.VehicleAvatar.Visible = false;
        vehicle.VehicleAvatar.Location = new Point(525, 380);
        vehicle.CurrentPos = vehicle.VehicleAvatar.Location;
        vehicle.VehicleAvatar.Size = new Size(100, 50);
        vehicle.VehicleAvatar.SizeMode = PictureBoxSizeMode.StretchImage;
        vehicle.VehicleAvatar.TabStop = false;
        vehicle.VehicleAvatar.BackColor = Color.Transparent;
    }

    public static void Move(Point end, ref Vehicle.Vehicle truck)
    {
        if (truck.TargetCity != null)
        {
            int count = truck.TargetCity.RoadLength / 10;
            var points = GetCoordsBetween(truck.CurrentPos, end, count);
            foreach (var point in points)
            {
                truck.VehicleAvatar.Location = new Point(point.X, point.Y);
                Thread.Sleep(100);
            }

            truck.CurrentPos = end;
        }
    }
}