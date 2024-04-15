namespace Logika
{
    public class PolozenieKulki : InterfejsPolozenia
    {
        private int _x;
        private int _y;
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
    }
}
