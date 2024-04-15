namespace Dane
{
    public class Kulka : InterfejsKulka
    {
        private int _x;
        private int _y;
        private readonly int _radius;

        public Kulka(int x, int y, int radius)
        {
            _x = x;
            _y = y;
            _radius = radius;
        }

        public int X 
        {
            get { return _x; }
            set
            {
                if (_x != value)
                {
                    _x = value;
                }
            }
        }

        public int Y
        {
            get { return _y; }
            set
            {
                if (_y != value)
                {
                    _y = value;
                }
            }
        }

        public int Radius 
        { 
            get { return _radius; }
        }
    }
}
