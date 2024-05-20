namespace TransportationLab2.Controller;

public static class Animation
{
    public static List<PictureBox> VehicleAvatars { get; set; } = [];
    public static TextBox MessageHandler { get; set; } = new();
    
    
    
    // Метод, который создаёт список точек, по которым грузовик начнёт своё движение 
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

    // Метод создания картинки для грузовика
    public static void CreateVehicleImage(ref Vehicle.Vehicle vehicle)
    {
        var vehicleAvatar = new PictureBox();
        string imageDirectory ="..\\..\\..\\View\\Resources\\truck.png"; 
        vehicleAvatar.Image = Image.FromFile(imageDirectory); // Находим файл (по относительной ссылке)
        vehicleAvatar.Visible = false; // На начальный момент грузовик не виден пользователю
        vehicleAvatar.Location = new Point(544, 426); // Точка старта (координаты Москвы на моей карте)
        vehicle.CurrentPos = vehicleAvatar.Location; // Ставим на это место картинку
        vehicleAvatar.Size = new Size(100, 50); 
        vehicleAvatar.SizeMode = PictureBoxSizeMode.StretchImage; // Подгоняем изображение под заданные размеры
        vehicleAvatar.Image.RotateFlip(RotateFlipType.RotateNoneFlipX);
        vehicleAvatar.BackColor = Color.Transparent;
        VehicleAvatars.Add(vehicleAvatar);
        
    }

    // Метод, который передвигает грузовик в указанную точку
    public static void Move(Point end, ref Vehicle.Vehicle truck, bool drivingBack)
    {
        if (truck.TargetCity != null)
        {
            if (drivingBack)
            {
                VehicleAvatars[truck.Id].Image.RotateFlip(RotateFlipType.RotateNoneFlipX);
            }
            // Количество точек зависит от длины дороги (для создания эффекта длительности передвижения)
            int count = truck.TargetCity.RoadLength / 10; 
            var points = GetCoordsBetween(truck.CurrentPos, end, count);
            foreach (var point in points)
            {
                VehicleAvatars[truck.Id].Location = new Point(point.X, point.Y);
                
                // Засыпание потока позволяет сделать процесс передвижения зависящим от времени
                Thread.Sleep(100); 
            }
            truck.CurrentPos = end;
            if (drivingBack)
            {
                VehicleAvatars[truck.Id].Image.RotateFlip(RotateFlipType.RotateNoneFlipX);
            }
        }
    }
}