using Logika;
using Dane;

namespace Testy
{
    public class LogikaTest
    {
        [TestMethod]
        public void InitializeTest()
        {
            InterfejsMenadzera menKulek = new MenadzerKulek();
            InterfejsLogic logika = new Logika.Logic(menKulek);

            logika.Initialize(100, 100, 2);
            for (int i = 0; i < 2; i++)
            {
                Assert.IsTrue(logika.GetPosition(i).X > 0 && logika.GetPosition(i).X < 100);
                Assert.IsTrue(logika.GetPosition(i).Y > 0 && logika.GetPosition(i).Y < 100);
            }
        }
    }
}
