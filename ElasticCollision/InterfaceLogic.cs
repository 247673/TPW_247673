using static ElasticCollision.Data;

namespace ElasticCollision
{
    public interface InterfaceLogic
    {
        public void CreateBalls(int numberOfBalls);
        void Move();
        List<Data.Ball> GetBalls();
        void UpdateBallPositions(Ball balls);
    }
}
