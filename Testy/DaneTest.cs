using Dane;

namespace Testy
{
    [TestClass]
    public class DaneTest
    {
        [TestMethod]
        public void CreateBallTest()
        {
            InterfejsMenadzera menKulek = new MenadzerKulek();
            menKulek.CreateNewBall(20, 5, 10);
            Assert.AreEqual(1, menKulek.GetBallCount());
            Assert.AreEqual(menKulek.GetBall(0).X, 20);
            Assert.AreEqual(menKulek.GetBall(0).Y, 5);
            Assert.AreEqual(menKulek.GetBall(0).Radius, 10);
        }

        [TestMethod]
        public void UpdateBallTest()
        {
            InterfejsMenadzera menKulek = new MenadzerKulek();
            menKulek.CreateNewBall(20, 5, 10);
            menKulek.UpdateBallStatus(0, 15, 1);
            Assert.AreEqual(menKulek.GetBall(0).X, 15);
            Assert.AreEqual(menKulek.GetBall(0).Y, 1);
        }
    }
}
