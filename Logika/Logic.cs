using Dane;

namespace Logika
{
    public interface ILogic
    {
        void CreateBall(double x, double y, double radius, double velocityX, double velocityY);
        List<(double x, double y, double radius, double velocityX, double velocityY)> GetBalls();
        void MoveBalls();
    }

    public class Logic : ILogic
    {
        private readonly IData _data;

        public Logic(IData data)
        {
            _data = data ?? throw new ArgumentNullException(nameof(data));
        }

        public void CreateBall(double x, double y, double radius, double velocityX, double velocityY)
        {
            _data.CreateBall(x, y, radius, velocityX, velocityY);
        }

        public List<(double x, double y, double radius, double velocityX, double velocityY)> GetBalls()
        {
            return _data.GetBalls();
        }

        public void MoveBalls()
        {
            _data.MoveBalls();
        }
    }
}
