using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reactive.Linq;
using System.Windows.Threading;
using Logika;

namespace Prezentacja
{
    public class ViewModel : INotifyPropertyChanged
    {
        private readonly ILogic _logic;
        private readonly Dispatcher _dispatcher = Dispatcher.CurrentDispatcher;
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

        public ViewModel(ILogic logic)
        {
            _logic = logic;
            _logic.CreateBalls(NumberOfBallsToGenerate);
            foreach (var ballData in _logic.GetBalls())
            {
                Model ballModel = new Model
                {
                    X = ballData.X,
                    Y = ballData.Y,
                    Radius = ballData.Radius,
                    VelocityX = ballData.VelocityX,
                    VelocityY = ballData.VelocityY,
                    Weight = ballData.Weight
                };
                Balls.Add(ballModel);
            }
            SetupInteractiveBehavior();
        }

        private void SetupInteractiveBehavior()
        {
            PropertyChanged += async (sender, args) =>
            {
                if (args.PropertyName == nameof(NumberOfBallsToGenerate))
                {
                    await Task.Run(() => Start());
                }
            };

            Task.Run(async () =>
            {
                while (true)
                {
                    await Task.Delay(TimeSpan.FromMilliseconds(16.67));
                    Move();
                }
            });
        }

        public void Start()
        {
            _dispatcher.Invoke(() =>
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
                        VelocityY = ballData.VelocityY,
                        Weight = ballData.Weight
                    };
                    Balls.Add(ballModel);
                }
            });
        }


        public void Move()
        {
            _logic.Move();

            foreach (var ball in Balls)
            {
                ball.X = _logic.GetBalls()[Balls.IndexOf(ball)].X;
                ball.Y = _logic.GetBalls()[Balls.IndexOf(ball)].Y;
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
