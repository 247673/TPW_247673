using Dane;

namespace Testy
{
    [TestClass]
    public class DaneTest
    {
        [TestMethod]
        public void GetAndSetTest()
        {
            // Arrange
            double expectedX = 10.0;
            double expectedY = 20.0;
            double expectedRadius = 5.0;
            double expectedVelocityX = 2.0;
            double expectedVelocityY = -3.0;
            double expectedWeight = 15.0;

            // Act
            Dane.Data.Ball ball = new Data.Ball();
            ball.X = expectedX;
            ball.Y = expectedY;
            ball.Radius = expectedRadius;
            ball.VelocityX = expectedVelocityX;
            ball.VelocityY = expectedVelocityY;
            ball.Weight = expectedWeight;

            // Assert
            Assert.AreEqual(expectedX, ball.X);
            Assert.AreEqual(expectedY, ball.Y);
            Assert.AreEqual(expectedRadius, ball.Radius);
            Assert.AreEqual(expectedVelocityX, ball.VelocityX);
            Assert.AreEqual(expectedVelocityY, ball.VelocityY);
            Assert.AreEqual(expectedWeight, ball.Weight);
        }
    }
}
