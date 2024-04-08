using System;
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
            _viewModel.Start();
        }
    }
}