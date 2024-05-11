namespace TransportationLab2.Manager;

public class Animation
{
    private object _moveLock = new();
    // private readonly VisualStyleElement.Window _window;
    // private readonly Canvas _canvas;
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
    
    private void MoveTo(Point end, Point currentPoint )
    {
        /*lock (_moveLock) // Захватываем данный объект(синхронизация)
        {
            try
            {
                var points = GetCoordsBetween(currentPoint, end, 20);
                foreach (var point in points)
                {
                    currentPoint = point;
                    // Так как WPF приложение работает с UI потоком,
                    // то Dispatcher осуществляет синхронизацию UI потока с созданными дополнительно
                    _window.Dispatcher.Invoke(() =>
                    {
                        Canvas.SetLeft(MyImage, currentPoint.X);
                        Canvas.SetTop(MyImage, currentPoint.Y);
                    });
                    Thread.Sleep(100);
                }
            }
            finally
            {
                State = tempState;
            }
        }*/
    }
}