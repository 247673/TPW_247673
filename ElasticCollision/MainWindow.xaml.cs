using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Threading;

namespace ElasticCollision
{
    public partial class MainWindow : Window
    {
        private readonly ViewModel _viewModel;
        private DispatcherTimer _timer;

        public MainWindow()
        {
            InitializeComponent();
            _viewModel = new ViewModel(new Logic());
            DataContext = _viewModel;
        }

        private void GenerateBallsButton_Click(object sender, RoutedEventArgs e)
        {
            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(16)
            };
            _timer.Tick += Timer_Tick;
            _timer.Start();
            _viewModel.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            _viewModel.Update();
        }
    }
}
