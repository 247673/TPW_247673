using Dane;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using static Dane.Data;
using Timer = System.Timers.Timer;

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
        private List<Data.Ball> _balls = new List<Data.Ball>();
        private List<Task> _tasks = new List<Task>();
        private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
        private CancellationTokenSource _loggingCancellationTokenSource = new CancellationTokenSource();
        private Logs _log;

        public Logic()
        {
            _log = new Logs(_balls, "..\\..\\..\\..\\log.json");
        }

        public async Task CreateBallsAsync(int numberOfBalls)
        {
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource = new CancellationTokenSource();
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
                    Weight = _random.Next(1, 5)
                };

                _balls.Add(ball);

                // Tworzenie osobnego zadania dla poruszania się każdej kuli
                Task task = Task.Run(() => MoveBallAsync(ball, _cancellationTokenSource.Token));
                _tasks.Add(task);
            }

            // Oczekiwanie na zakończenie wszystkich zadań
            await Task.WhenAll(_tasks);
        }

        private async Task MoveBallAsync(Data.Ball ball, CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                Move(ball);
                await Task.Delay(TimeSpan.FromMilliseconds(16.67), cancellationToken);
            }
        }

        private void Move(Data.Ball ball)
        {
            ball.X += ball.VelocityX;
            ball.Y += ball.VelocityY;

            CheckWallCollision(ball);

            lock (_balls) // Blokada wokół operacji na liście kul
            {
                foreach (var otherBall in _balls)
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
            double dx = ball2.X - ball1.X;
            double dy = ball2.Y - ball1.Y;
            double distance = Math.Sqrt(dx * dx + dy * dy);

            if (distance == 0) return; // Prevent division by zero

            double vX1 = (ball1.VelocityX * (ball1.Weight - ball2.Weight) + 2 * ball2.Weight * ball2.VelocityX) / (ball1.Weight + ball2.Weight);
            double vY1 = (ball1.VelocityY * (ball1.Weight - ball2.Weight) + 2 * ball2.Weight * ball2.VelocityY) / (ball1.Weight + ball2.Weight);
            double vX2 = (ball2.VelocityX * (ball2.Weight - ball1.Weight) + 2 * ball1.Weight * ball1.VelocityX) / (ball1.Weight + ball2.Weight);
            double vY2 = (ball2.VelocityY * (ball2.Weight - ball1.Weight) + 2 * ball1.Weight * ball1.VelocityY) / (ball1.Weight + ball2.Weight);

            ball1.VelocityX = vX1;
            ball1.VelocityY = vY1;
            ball2.VelocityX = vX2;
            ball2.VelocityY = vY2;
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
            return _balls.ToList(); // Convert list to List for returning
        }
    }
}