using System.Drawing.Imaging;

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

    public static void CreateImage(Vehicle.Vehicle vehicle)
    {
        vehicle.VehicleAvatar.Image = Image.FromFile("C:\\Users\\leoni\\OneDrive\\Рабочий стол\\" +
                                                     "My Labs\\CSharp\\TransportationLab2\\Resources\\truck.png");
        vehicle.VehicleAvatar.Visible = true;
        vehicle.VehicleAvatar.Location = new Point(505, 380);
        vehicle.VehicleAvatar.Size = new Size(100, 50);
        vehicle.VehicleAvatar.SizeMode = PictureBoxSizeMode.StretchImage;
        vehicle.VehicleAvatar.TabStop = false;
        vehicle.VehicleAvatar.BackColor = Color.Transparent;

        /*
           mskPictureBox.BackColor = Color.Transparent;
           mskPictureBox.BackgroundImageLayout = ImageLayout.None;
           mskPictureBox.Image = (Image)resources.GetObject("mskPictureBox.Image");
           mskPictureBox.Location = new Point(525, 380);
           mskPictureBox.Name = "mskPictureBox";
           mskPictureBox.Size = new Size(39, 62);
           mskPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
           mskPictureBox.TabIndex = 13;
           mskPictureBox.TabStop = false;
         */
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