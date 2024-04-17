using Dane;
using Logika;

namespace Testy
{
    [TestClass]
    public class LogikaTest
    {
        [TestMethod]
        public void TestCreateBalls()
        {
            // Arrange
            Logic logic = new Logic();
            int numberOfBalls = 5;

            // Act
            logic.CreateBalls(numberOfBalls);
            List<Data.Ball> balls = logic.GetBalls();

            // Assert
            Assert.AreEqual(numberOfBalls, balls.Count);
            foreach (var ball in balls)
            {
                Assert.IsTrue(ball.X >= 5 && ball.X <= 695); // Sprawdzenie zakresu dla współrzędnych X
                Assert.IsTrue(ball.Y >= 5 && ball.Y <= 295); // Sprawdzenie zakresu dla współrzędnych Y
                Assert.IsTrue(ball.VelocityX >= -3 && ball.VelocityX <= 3); // Sprawdzenie zakresu prędkości X
                Assert.IsTrue(ball.VelocityY >= -3 && ball.VelocityY <= 3); // Sprawdzenie zakresu prędkości Y
            }
        }

        [TestMethod]
        public void TestUpdateBallPositions()
        {
            // Arrange
            Logic logic = new Logic();
            Data.Ball ball = new Data.Ball() { X = 700, Y = 300, Radius = 5, VelocityX = 1, VelocityY = 1 };

            // Act
            logic.UpdateBallPositions(ball);

            // Assert
            Assert.IsTrue(ball.X >= 5 && ball.X <= 695); // Sprawdzenie zakresu dla współrzędnych X
            Assert.IsTrue(ball.Y >= 5 && ball.Y <= 295); // Sprawdzenie zakresu dla współrzędnych Y
        }

    }
}
