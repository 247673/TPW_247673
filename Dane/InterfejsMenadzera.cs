namespace Dane
{
    public interface InterfejsMenadzera
    {
        void CreateNewBall(int x, int y, int radius);
        void UpdateBallStatus(int index, int x, int y);
        InterfejsKulka GetBall(int index);
        int GetBallCount();
    }
}
