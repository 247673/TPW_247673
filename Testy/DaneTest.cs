using Dane;

namespace Testy
{
    [TestClass]
    public class DaneTest
    {
        [TestMethod]
        public void TestCreateBall()
        {
            // Arrange
            var dataService = new Data();
            var expectedX = 10.0;
            var expectedY = 20.0;
            var expectedRadius = 5.0;
            var expectedVelocityX = 1.0;
            var expectedVelocityY = 2.0;

            // Act
            dataService.CreateBall(expectedX, expectedY, expectedRadius, expectedVelocityX, expectedVelocityY);
            var balls = dataService.GetBalls();

            // Assert
            Assert.IsNotNull(balls);
            Assert.AreEqual(1, balls.Count);
            Assert.AreEqual(expectedX, balls[0].x);
            Assert.AreEqual(expectedY, balls[0].y);
            Assert.AreEqual(expectedRadius, balls[0].radius);
            Assert.AreEqual(expectedVelocityX, balls[0].velocityX);
            Assert.AreEqual(expectedVelocityY, balls[0].velocityY);
        }

        [TestMethod]
        public void TestMoveBalls()
        {
            // Arrange
            var dataService = new Data();
            var initialX = 10.0;
            var initialY = 20.0;
            var velocityX = 1.0;
            var velocityY = 2.0;
            var radius = 5.0;
            dataService.CreateBall(initialX, initialY, radius, velocityX, velocityY);

            // Act
            dataService.MoveBalls();
            var balls = dataService.GetBalls();

            // Assert
            Assert.IsNotNull(balls);
            Assert.AreEqual(1, balls.Count);
            Assert.AreEqual(initialX + velocityX, balls[0].x);
            Assert.AreEqual(initialY + velocityY, balls[0].y);
        }
    }
}
