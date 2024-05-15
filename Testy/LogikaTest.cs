using Dane;
using Logika;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Testy
{
    [TestClass]
    public class LogikaTest
    {
        [TestMethod]
        public async Task CreateBallsAsync_CreatesCorrectNumberOfBalls()
        {
            // Arrange
            ILogic logic = new Logic();
            int numberOfBalls = 5;

            // Act
            await logic.CreateBallsAsync(numberOfBalls);
            List<Data.Ball> balls = logic.GetBalls();

            // Assert
            Assert.AreEqual(numberOfBalls, balls.Count);
        }

        [TestMethod]
        public async Task CreateBallsAsync_BallsHaveCorrectProperties()
        {
            // Arrange
            ILogic logic = new Logic();
            int numberOfBalls = 1;

            // Act
            await logic.CreateBallsAsync(numberOfBalls);
            List<Data.Ball> balls = logic.GetBalls();
            Data.Ball ball = balls.FirstOrDefault();

            // Assert
            Assert.IsNotNull(ball);
            Assert.IsTrue(ball.Radius == 5);
            Assert.IsTrue(ball.X >= 5 && ball.X <= 695);
            Assert.IsTrue(ball.Y >= 5 && ball.Y <= 250);
            Assert.IsTrue(ball.VelocityX >= -3 && ball.VelocityX <= 3);
            Assert.IsTrue(ball.VelocityY >= -3 && ball.VelocityY <= 3);
            Assert.IsTrue(ball.Weight >= 1 && ball.Weight <= 9);
        }

        [TestMethod]
        public async Task CreateBallsAsync_NoOverlapBetweenBalls()
        {
            // Arrange
            ILogic logic = new Logic();
            int numberOfBalls = 5;

            // Act
            await logic.CreateBallsAsync(numberOfBalls);
            List<Data.Ball> balls = logic.GetBalls();

            // Assert
            foreach (var ball1 in balls)
            {
                foreach (var ball2 in balls)
                {
                    if (ball1 != ball2)
                    {
                        double distance = Math.Sqrt(Math.Pow(ball2.X - ball1.X, 2) + Math.Pow(ball2.Y - ball1.Y, 2));
                        Assert.IsTrue(distance > ball1.Radius + ball2.Radius);
                    }
                }
            }
        }
    }
}
