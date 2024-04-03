using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using System.Linq;
using static ElasticCollision.Data;
using System;

namespace ElasticCollision
{
    public class ViewModel : INotifyPropertyChanged
    {
        private readonly InterfaceLogic _logic;

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<Model> Balls { get; } = new ObservableCollection<Model>();

        private int _numberOfBallsToGenerate;

        public ViewModel(InterfaceLogic logic)
        {
            _logic = logic;
        }

        public int NumberOfBallsToGenerate
        {
            get { return _numberOfBallsToGenerate; }
            set
            {
                _numberOfBallsToGenerate = value;
                OnPropertyChanged(nameof(NumberOfBallsToGenerate));
            }
        }

        public void Start()
        {
            _logic.CreateBalls(NumberOfBallsToGenerate);
            Update();
            StartTimer(); // Rozpocznij timer po utworzeniu kulek
        }

        public void Update()
        {
            Balls.Clear();
            foreach (var ball in _logic.GetBalls()) // Użyj nowej metody GetBalls()
            {
                Balls.Add(new Model
                {
                    X = ball.X,
                    Y = ball.Y,
                    Radius = ball.Radius,
                    VelocityX = ball.VelocityX,
                    VelocityY = ball.VelocityY
                });
            }
        }

        // Timer do aktualizacji pozycji kul
        private void StartTimer()
        {
            System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();
            timer.Interval = System.TimeSpan.FromMilliseconds(16.67); // Około 60 razy na sekundę
            timer.Tick += (sender, e) =>
            {
                _logic.Move(); // Wywołaj metodę Move z klasy Logic
                Update(); // Aktualizuj widok
            };
            timer.Start();
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
