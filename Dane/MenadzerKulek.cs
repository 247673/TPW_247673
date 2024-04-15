namespace Dane
{
    public class MenadzerKulek : InterfejsMenadzera    
    {
        private List<InterfejsKulka> _balls = new List<InterfejsKulka>();

        public void CreateNewBall(int x, int y, int radius)
        {
            _balls.Add(new Kulka(x, y, radius));
        }

        public InterfejsKulka GetBall(int index)
        {
            if (index >= _balls.Count)
            {
                throw new IndexOutOfRangeException("Wrong index");
            }
            return _balls[index];
        }

        public int GetBallCount()
        {
            return _balls.Count;
        }

        public void UpdateBallStatus(int index, int x, int y)
        {
            if (index >= _balls.Count || index < 0)
            {
                throw new IndexOutOfRangeException("Wrong index");
            }
            _balls[index].X = x;
            _balls[index].Y = y;
        }
    }
}
