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
            Logic logic = new Logic();
            int numberOfBalls = 5;

            logic.CreateBalls(numberOfBalls);
            List<Data.Ball> balls = logic.GetBalls();

            Assert.AreEqual(numberOfBalls, balls.Count);
            foreach (var ball in balls)
            {
                Assert.IsTrue(ball.X >= 5 && ball.X <= 695);
                Assert.IsTrue(ball.Y >= 5 && ball.Y <= 295);
                Assert.IsTrue(ball.VelocityX >= -3 && ball.VelocityX <= 3);
                Assert.IsTrue(ball.VelocityY >= -3 && ball.VelocityY <= 3);
            }
        }

        [TestMethod]
        public void TestMove_BallsMoveAccordingToVelocity()
        {
            // Arrange
            ILogic logic = new Logic();
            logic.CreateBalls(1);
            Data.Ball ball = logic.GetBalls()[0];

            double initialX = ball.X;
            double initialY = ball.Y;

            logic.Move();

            Assert.AreNotEqual(initialX, ball.X);
            Assert.AreNotEqual(initialY, ball.Y);
        }

        [TestMethod]
        public void TestMove_BallsBounceOffWalls()
        {
            ILogic logic = new Logic();
            logic.CreateBalls(4);
            List<Data.Ball> balls = logic.GetBalls();

            balls[0].X = balls[0].Radius + 1; // Górny lewy róg
            balls[0].Y = balls[0].Radius + 1;
            balls[0].VelocityX = -1;
            balls[0].VelocityY = -1;

            balls[1].X = 700 - balls[1].Radius - 1; // Górny prawy róg
            balls[1].Y = balls[1].Radius + 1;
            balls[1].VelocityX = 1;
            balls[1].VelocityY = -1;

            balls[2].X = balls[2].Radius + 1; // Dolny lewy róg
            balls[2].Y = 300 - balls[2].Radius - 1;
            balls[2].VelocityX = -1;
            balls[2].VelocityY = 1;

            balls[3].X = 700 - balls[3].Radius - 1; // Dolny prawy róg
            balls[3].Y = 300 - balls[3].Radius - 1;
            balls[3].VelocityX = 1;
            balls[3].VelocityY = 1;

            logic.Move();

            Assert.IsTrue(balls[0].VelocityX > 0);
            Assert.IsTrue(balls[0].VelocityY > 0);

            Assert.IsTrue(balls[1].VelocityX < 0);
            Assert.IsTrue(balls[1].VelocityY > 0);

            Assert.IsTrue(balls[2].VelocityX > 0);
            Assert.IsTrue(balls[2].VelocityY < 0);

            Assert.IsTrue(balls[3].VelocityX < 0);
            Assert.IsTrue(balls[3].VelocityY < 0);
        }

        [TestMethod]
        public void TestMove_BallsBounceOffEachOther()
        {
            Logic logic = new Logic();
            logic.CreateBalls(4);
            List<Data.Ball> balls = logic.GetBalls();
            balls[0].X = 15;
            balls[0].Y = 10;
            balls[0].VelocityX = 1;
            balls[0].VelocityY = 0;
            balls[0].Weight = 10;

            balls[1].X = 20;
            balls[1].Y = 10;
            balls[1].VelocityX = -1;
            balls[1].VelocityY = 0;
            balls[1].Weight = 5;

            balls[2].X = 10;
            balls[2].Y = 20;
            balls[2].VelocityX = 0;
            balls[2].VelocityY = 1;
            balls[2].Weight = 10;

            balls[3].X = 10;
            balls[3].Y = 15;
            balls[3].VelocityX = 0;
            balls[3].VelocityY = -1;
            balls[3].Weight = 5;

            logic.Move();

            Assert.IsTrue(balls[0].VelocityX < 1);
            Assert.IsTrue(balls[1].VelocityX > -1);
            Assert.IsTrue(balls[2].VelocityY < 1);
            Assert.IsTrue(balls[3].VelocityY > -1);
        }

    }
}
