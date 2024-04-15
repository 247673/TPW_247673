using Dane;

namespace Logika
{
    public class Logic : InterfejsLogic
    {
        private InterfejsMenadzera menKulek;
        private int _iloscKulek;
        private int _szerokosc;
        private int _wysokosc;
        private int _promienKulek;

        public Logic(InterfejsMenadzera menadzerKulek)
        {
            menKulek = menadzerKulek;
        }

        public InterfejsPolozenia GetPosition(int index)
        {
            if (index >= _iloscKulek || index < 0)
            {
                throw new IndexOutOfRangeException("Wrong index");
            }
            return new PolozenieKulki { X = menKulek.GetBall(index).X, Y = menKulek.GetBall(index).Y };
        }

        private void MoveBall(object obj)
        {
            int index = (int)obj;
            Random random = new Random();
            int kierunekX;
            int kierunekY;
            while (true)
            {
                kierunekX = random.Next(-3, 3);
                kierunekY = random.Next(-3, 3);
                menKulek.GetBall(index).X += kierunekX;
                menKulek.GetBall(index).Y += kierunekY;
                if (menKulek.GetBall(index).X > 100 || menKulek.GetBall(index).X < 10 || menKulek.GetBall(index).Y > 100 || menKulek.GetBall(index).Y < 10)
                {
                    break;
                }
            }
        }

        public void Initialize(int szerokosc, int wysokosc, int iloscKulek)
        {
            _iloscKulek = iloscKulek;
            _szerokosc = szerokosc;
            _wysokosc = wysokosc;
            _promienKulek = 10;
            Random random = new Random();
            for (int i = 0; i < _iloscKulek; i++)
            {
                menKulek.CreateNewBall(random.Next(100) + _promienKulek, random.Next(100) + _promienKulek, _promienKulek);
            }
        }
    }
}
