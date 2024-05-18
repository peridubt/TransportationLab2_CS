namespace TransportationLab2.Controller;

public static class Animation
{
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
        string imageDirectory ="..\\..\\..\\Resources\\truck.png"; 
        vehicle.VehicleAvatar.Image = Image.FromFile(imageDirectory); // Находим файл (по относительной ссылке)
        vehicle.VehicleAvatar.Visible = false; // На начальный момент грузовик не виден пользователю
        vehicle.VehicleAvatar.Location = new Point(544, 426); // Точка старта (координаты Москвы на моей карте)
        vehicle.CurrentPos = vehicle.VehicleAvatar.Location; // Ставим на это место картинку
        vehicle.VehicleAvatar.Size = new Size(100, 50); 
        vehicle.VehicleAvatar.SizeMode = PictureBoxSizeMode.StretchImage; // Подгоняем изображение под заданные размеры
        vehicle.VehicleAvatar.TabStop = false;
    }

    // Метод, который передвигает грузовик в указанную точку
    public static void Move(Point end, ref Vehicle.Vehicle truck)
    {
        if (truck.TargetCity != null)
        {
            // Количество точек зависит от длины дороги (для создания эффекта длительности передвижения)
            int count = truck.TargetCity.RoadLength / 15; 
            var points = GetCoordsBetween(truck.CurrentPos, end, count);
            foreach (var point in points)
            {
                truck.VehicleAvatar.Location = new Point(point.X, point.Y);
                
                // Засыпание потока позволяет сделать процесс передвижения зависящим от времени
                Thread.Sleep(100); 
            }

            truck.CurrentPos = end;
        }
    }
}