using Dane;
using Logika;
using System.Reflection;
using static Dane.Data;

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
            Task createBallsTask = logic.CreateBallsAsync(numberOfBalls);
            List<Data.Ball> balls = logic.GetBalls();

            Assert.AreEqual(numberOfBalls, balls.Count);
            foreach (var ball in balls)
            {
                Assert.IsTrue(ball.X >= 0 && ball.X <= 700);
                Assert.IsTrue(ball.Y >= 0 && ball.Y <= 300);
                Assert.IsTrue(ball.VelocityX >= -3 && ball.VelocityX <= 3);
                Assert.IsTrue(ball.VelocityY >= -3 && ball.VelocityY <= 3);
            }
        }

        [TestMethod]
        public void TestMove_BallsMoveAccordingToVelocity()
        {
            ILogic logic = new Logic();
            Task createBallsTask = logic.CreateBallsAsync(1);
            Data.Ball ball = logic.GetBalls()[0];
            double initialX = ball.X;
            double initialY = ball.Y;

            // Wywołanie metody Move przy użyciu refleksji
            MethodInfo moveMethod = typeof(Logic).GetMethod("Move", BindingFlags.NonPublic | BindingFlags.Instance);
            moveMethod.Invoke(logic, new object[] { ball });

            Assert.AreNotEqual(initialX, ball.X);
            Assert.AreNotEqual(initialY, ball.Y);
        }

        [TestMethod]
        public void TestMove_BallsBounceOffWalls()
        {
            ILogic logic = new Logic();
            Task createBallsTask = logic.CreateBallsAsync(4);
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

            // Uzyskaj metodę Move za pomocą refleksji
            MethodInfo moveMethod = typeof(Logic).GetMethod("Move", BindingFlags.NonPublic | BindingFlags.Instance);
            // Wywołaj metodę Move dla każdej piłki
            foreach (var ball in balls)
            {
                moveMethod.Invoke(logic, new object[] { ball });
            }

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
            Task createBallsTask = logic.CreateBallsAsync(4);
            List<Data.Ball> balls = logic.GetBalls();
            balls[0].X = 15;
            balls[0].Y = 10;
            balls[0].VelocityX = 1;
            balls[0].VelocityY = 0;
            balls[0].Weight = 5;

            balls[1].X = 20;
            balls[1].Y = 10;
            balls[1].VelocityX = -1;
            balls[1].VelocityY = 0;
            balls[1].Weight = 1;

            balls[2].X = 10;
            balls[2].Y = 20;
            balls[2].VelocityX = 0;
            balls[2].VelocityY = 1;
            balls[2].Weight = 5;

            balls[3].X = 10;
            balls[3].Y = 15;
            balls[3].VelocityX = 0;
            balls[3].VelocityY = -1;
            balls[3].Weight = 5;

            // Uzyskaj metodę Move za pomocą refleksji
            MethodInfo moveMethod = typeof(Logic).GetMethod("Move", BindingFlags.NonPublic | BindingFlags.Instance);
            // Wywołaj metodę Move dla każdej piłki
            foreach (var ball in balls)
            {
                moveMethod.Invoke(logic, new object[] { ball });
            }

            Assert.IsTrue(balls[0].VelocityX < 1);
            Assert.IsTrue(balls[1].VelocityX < -1);
            Assert.IsTrue(balls[2].VelocityY < 1);
            Assert.IsTrue(balls[3].VelocityY > -1);
        }
    }
}