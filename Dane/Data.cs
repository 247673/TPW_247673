using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading;

namespace Dane
{
    public class Data
    {
        public class Ball
        {
            public double X { get; set; }
            public double Y { get; set; }
            public double Radius { get; set; }
            public double VelocityX { get; set; }
            public double VelocityY { get; set; }
            public double Weight { get; set; }
        }

        private List<Ball> balls = new List<Ball>();
        private readonly object lockObj = new object();

        public void AddBall(Ball ball)
        {
            lock (lockObj)
            {
                balls.Add(ball);
            }
        }

        public List<Ball> GetBalls()
        {
            lock (lockObj)
            {
                return new List<Ball>(balls);
            }
        }
    }
}