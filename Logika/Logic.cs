using Dane;

namespace Logika
{
    public interface ILogic
    {
        public void CreateBalls(int numberOfBalls);
        void Move();
        List<Data.Ball> GetBalls();
        void UpdateBallPositions(Data.Ball balls);
    }

    public class Logic : ILogic
    {
        private readonly Random _random = new Random();
        private const int CanvasWidth = 700;
        private const int CanvasHeight = 300;
        private readonly List<Data.Ball> balls = new List<Data.Ball>();

        public void CreateBalls(int numberOfBalls)
        {
            balls.Clear();
            for (int i = 0; i < numberOfBalls; i++)
            {
                balls.Add(new Data.Ball
                {
                    Radius = 5,
                    X = _random.Next(5, CanvasWidth - 5), // Ustalamy zakres dla współrzędnych X
                    Y = _random.Next(5, CanvasHeight - 5), // Ustalamy zakres dla współrzędnych Y
                    VelocityX = (_random.NextDouble() * 6 - 3), // Ustalamy zakres prędkości X
                    VelocityY = (_random.NextDouble() * 6 - 3) // Ustalamy zakres prędkości Y
                });
            }
        }

        public void Move()
        {
            foreach (var ball in balls)
            {
                ball.X += ball.VelocityX;
                ball.Y += ball.VelocityY;
                UpdateBallPositions(ball);
            }
        }

        public List<Data.Ball> GetBalls()
        {
            return balls;
        }

        public void UpdateBallPositions(Data.Ball ball)
        {
            // Przenoszenie kulki na nową losową pozycję, gdy wyleci poza Canvas
            if (ball.X - ball.Radius <= 0 || ball.X + ball.Radius >= CanvasWidth)
            {
                ball.X = _random.Next(5, CanvasWidth - 5);
            }
            if (ball.Y - ball.Radius <= 0 || ball.Y + ball.Radius >= CanvasHeight)
            {
                ball.Y = _random.Next(5, CanvasHeight - 5);
            }
        }
    }
}