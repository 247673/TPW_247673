using Dane;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Logika
{
    public interface ILogic
    {
        Task CreateBallsAsync(int numberOfBalls);
        List<Data.Ball> GetBalls();
    }

    public class Logic : ILogic
    {
        private readonly Random _random = new Random();
        private const int CanvasWidth = 700;
        private const int CanvasHeight = 255;
        private readonly List<Data.Ball> _balls = new List<Data.Ball>();
        private readonly List<Task> _tasks = new List<Task>();

        public async Task CreateBallsAsync(int numberOfBalls)
        {
            _balls.Clear();
            _tasks.Clear();

            for (int i = 0; i < numberOfBalls; i++)
            {
                Data.Ball ball = new Data.Ball
                {
                    Radius = 5,
                    X = _random.Next(5, CanvasWidth - 5),
                    Y = _random.Next(5, CanvasHeight - 5),
                    VelocityX = (_random.NextDouble() * 6 - 3),
                    VelocityY = (_random.NextDouble() * 6 - 3),
                    Weight = _random.Next(1, 10)
                };

                _balls.Add(ball);

                // Tworzenie osobnego zadania dla poruszania się każdej kuli
                Task task = Task.Run(() => MoveBallAsync(ball));
                _tasks.Add(task);
            }

            // Oczekiwanie na zakończenie wszystkich zadań
            await Task.WhenAll(_tasks);
        }

        private async Task MoveBallAsync(Data.Ball ball)
        {
            while (true)
            {
                Move(ball);
                await Task.Delay(TimeSpan.FromMilliseconds(16.67));
            }
        }

        private void Move(Data.Ball ball)
        {
            ball.X += ball.VelocityX;
            ball.Y += ball.VelocityY;

            CheckWallCollision(ball);

            foreach (var otherBall in _balls)
            {
                if (ball != otherBall && AreColliding(ball, otherBall))
                {
                    CalculateCollision(ball, otherBall);
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
            return _balls;
        }
    }
}
