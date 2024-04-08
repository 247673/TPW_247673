using System.Collections.ObjectModel;
using System.ComponentModel;

namespace ElasticCollision
{
    public class ViewModel : INotifyPropertyChanged
    {
        private readonly InterfaceLogic _logic;
        private const int CanvasWidth = 700;
        private const int CanvasHeight = 300;

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<Model> Balls { get; } = new ObservableCollection<Model>();

        private int _numberOfBallsToGenerate;

        public int NumberOfBallsToGenerate
        {
            get { return _numberOfBallsToGenerate; }
            set
            {
                _numberOfBallsToGenerate = value;
                OnPropertyChanged(nameof(NumberOfBallsToGenerate));
            }
        }

        public ViewModel(InterfaceLogic logic)
        {
            _logic = logic;
        }

        public void Start()
        {
            Balls.Clear();
            _logic.CreateBalls(NumberOfBallsToGenerate);
            foreach (var ballData in _logic.GetBalls())
            {
                Model ballModel = new Model
                {
                    X = ballData.X,
                    Y = ballData.Y,
                    Radius = ballData.Radius,
                    VelocityX = ballData.VelocityX,
                    VelocityY = ballData.VelocityY
                };
                Balls.Add(ballModel);
            }
            StartTimer();
        }

        private void StartTimer()
        {
            System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(16.67);
            timer.Tick += (sender, e) =>
            {
                Move();
            };
            timer.Start();
        }

        public void Move()
        {
            foreach (var ball in Balls)
            {
                ball.X += ball.VelocityX;
                ball.Y += ball.VelocityY;
                UpdateBallPositions(ball);
            }

            OnPropertyChanged(nameof(Balls)); // Odświeżenie widoku po przesunięciu kulek
        }


        private void UpdateBallPositions(Model ball)
        {
            if (ball.X - ball.Radius <= 0 || ball.X + ball.Radius >= CanvasWidth)
            {
                ball.VelocityX = -ball.VelocityX;
            }

            if (ball.Y - ball.Radius <= 0 || ball.Y + ball.Radius >= CanvasHeight)
            {
                ball.VelocityY = -ball.VelocityY;
            }
        }


        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
