using Dane;
using Logika;

namespace Testy
{
    [TestClass]
    public class LogikaTest
    {
        [TestMethod]
        public void CreateBall_AddsNewBallToList()
        {
            // Arrange
            IData data = new MockDataService();
            ILogic logic = new Logic(data);
            int initialCount = logic.GetBalls().Count;

            // Act
            logic.CreateBall(0, 0, 10, 1, 1);

            // Assert
            Assert.AreEqual(initialCount + 1, logic.GetBalls().Count);
        }

        [TestMethod]
        public void MoveBalls_UpdatesBallPositions()
        {
            // Arrange
            IData data = new MockDataService();
            ILogic logic = new Logic(data);
            logic.CreateBall(0, 0, 10, 1, 1);

            // Act
            logic.MoveBalls();
            var balls = logic.GetBalls();

            // Assert
            Assert.AreEqual(1, balls.Count);
            Assert.AreEqual(1, balls[0].x); // oczekujemy, że pozycja X kuli została zaktualizowana
            Assert.AreEqual(1, balls[0].y); // oczekujemy, że pozycja Y kuli została zaktualizowana
        }
    }

    // Mock implementacja interfejsu IDataService dla testów
    public class MockDataService : IData
    {
        private List<(double x, double y, double radius, double velocityX, double velocityY)> balls;

        public MockDataService()
        {
            balls = new List<(double x, double y, double radius, double velocityX, double velocityY)>();
        }

        public void CreateBall(double x, double y, double radius, double velocityX, double velocityY)
        {
            balls.Add((x, y, radius, velocityX, velocityY));
        }

        public List<(double x, double y, double radius, double velocityX, double velocityY)> GetBalls()
        {
            return balls;
        }

        public void MoveBalls()
        {
            for (int i = 0; i < balls.Count; i++)
            {
                var ball = balls[i];
                ball.x += ball.velocityX;
                ball.y += ball.velocityY;
                balls[i] = ball; // Aktualizacja kuli w liście
            }
        }
    }
}
