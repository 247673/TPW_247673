namespace Dane
{
    // Interfejs IDataService
    public interface IData
    {
        void CreateBall(double x, double y, double radius, double velocityX, double velocityY); // Metoda do tworzenia kuli
        List<(double x, double y, double radius, double velocityX, double velocityY)> GetBalls(); // Metoda do pobierania pozycji wszystkich kul
        void MoveBalls(); // Metoda do aktualizacji pozycji kul na podstawie ich prędkości
    }

    // Model Ball z prędkościami X i Y
    public class Ball
    {
        public double X { get; set; } // Pozycja X
        public double Y { get; set; } // Pozycja Y
        public double Radius { get; set; } // Promień
        public double VelocityX { get; set; } // Prędkość w kierunku X
        public double VelocityY { get; set; } // Prędkość w kierunku Y
    }

    // Klasa DataService implementująca IDataService
    public class Data : IData
    {
        private List<Ball> balls;

        public Data()
        {
            balls = new List<Ball>();
        }

        public void CreateBall(double x, double y, double radius, double velocityX, double velocityY)
        {
            balls.Add(new Ball { X = x, Y = y, Radius = radius, VelocityX = velocityX, VelocityY = velocityY });
        }

        public List<(double x, double y, double radius, double velocityX, double velocityY)> GetBalls()
        {
            var result = new List<(double x, double y, double radius, double velocityX, double velocityY)>();
            foreach (var ball in balls)
            {
                result.Add((ball.X, ball.Y, ball.Radius, ball.VelocityX, ball.VelocityY));
            }
            return result;
        }

        public void MoveBalls()
        {
            foreach (var ball in balls)
            {
                ball.X += ball.VelocityX;
                ball.Y += ball.VelocityY;
            }
        }
    }
}
