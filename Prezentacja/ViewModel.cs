using Logika;
using Prezentacja;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Threading;

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
            if (_numberOfBallsToGenerate != value)
            {
                _numberOfBallsToGenerate = value;
                OnPropertyChanged(nameof(NumberOfBallsToGenerate));
                UpdateBallsAsync();
            }
        }
    }

    public ViewModel(ILogic logic)
    {
        _logic = logic;
        _logic.StartLogging("C:\\Users\\Miki\\Desktop\\sem4\\TPW\\TPW_247673\\Log\\log.json");
        UpdateBallsAsync();
        SetupInteractiveBehavior();
    }

    private async Task UpdateBallsAsync()
    {
        await _logic.CreateBallsAsync(NumberOfBallsToGenerate);
        UpdateBallsUI();
    }

    private void SetupInteractiveBehavior()
    {
        Task.Run(async () =>
        {
            while (true)
            {
                await Task.Delay(TimeSpan.FromMilliseconds(16.67));
                Move();
            }
        });
    }

    public void Move()
    {
        UpdateBallsUI();
    }

    private void UpdateBallsUI()
    {
        _dispatcher.Invoke(() =>
        {
            Balls.Clear();
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
    public void Stop()
    {
        _logic.Stop();
    }
    public void ClearLog()
    {
        _logic.ClearLog();
    }

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}