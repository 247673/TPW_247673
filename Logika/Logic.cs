using Dane;

namespace Logika
{
    public interface ILogic
    {
        public void CreateBalls(int numberOfBalls);
        void Move();
        List<Data.Ball> GetBalls();
    }

    public class Logic : ILogic
    {
        private readonly Random _random = new Random();
        private const int CanvasWidth = 700;
        private const int CanvasHeight = 255;
        private readonly List<Data.Ball> balls = new List<Data.Ball>();

        public void CreateBalls(int numberOfBalls)
        {
            balls.Clear();
            for (int i = 0; i < numberOfBalls; i++)
            {
                balls.Add(new Data.Ball
                {
                    Radius = 5,
                    X = _random.Next(5, CanvasWidth - 5),
                    Y = _random.Next(5, CanvasHeight - 5),
                    VelocityX = (_random.NextDouble() * 6 - 3),
                    VelocityY = (_random.NextDouble() * 6 - 3),
                    Weight = _random.Next(1, 10)
                });
            }
        }

        public void Move()
        {
            foreach (var ball in balls)
            {
                ball.X += ball.VelocityX;
                ball.Y += ball.VelocityY;
                CheckWallCollision(ball);

                foreach (var otherBall in balls)
                {
                    if (ball != otherBall && AreColliding(ball, otherBall))
                    {
                        CalculateCollision(ball, otherBall);
                    }
                }
            }
        }

        private bool AreColliding(Data.Ball ball1, Data.Ball ball2)
        {
            double dx = ball2.X - ball1.X;
            double dy = ball2.Y - ball1.Y;
            double distance = Math.Sqrt(dx * dx + dy * dy);
            return distance <= ball1.Radius + ball2.Radius;
        }

        private void CalculateCollision(Data.Ball ball1, Data.Ball ball2)
        {
            double dvx = ball2.VelocityX - ball1.VelocityX;
            double dvy = ball2.VelocityY - ball1.VelocityY;

            double totalMass = ball1.Weight + ball2.Weight;

            double deltaVx1 = (2 * ball2.Weight * dvx) / totalMass;
            double deltaVy1 = (2 * ball2.Weight * dvy) / totalMass;
            double deltaVx2 = (2 * ball1.Weight * dvx) / totalMass;
            double deltaVy2 = (2 * ball1.Weight * dvy) / totalMass;

            ball1.VelocityX += deltaVx1;
            ball1.VelocityY += deltaVy1;
            ball2.VelocityX -= deltaVx2;
            ball2.VelocityY -= deltaVy2;
        }



        private void CheckWallCollision(Data.Ball ball)
        {
            if (ball.X - ball.Radius <= 0 || ball.X + ball.Radius >= CanvasWidth)
            {
                ball.VelocityX = -ball.VelocityX;
            }

            if (ball.Y - ball.Radius <= 0 || ball.Y + ball.Radius >= CanvasHeight)
            {
                ball.VelocityY = -ball.VelocityY;
            }
        }

        public List<Data.Ball> GetBalls()
        {
            return balls;
        }

    }
}