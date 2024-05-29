﻿using Logika;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Prezentacja
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ViewModel _viewModel;

        public MainWindow()
        {
            InitializeComponent();
            _viewModel = new ViewModel(new Logic());
            DataContext = _viewModel;
        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.Stop();
        }
        private void ClearLog_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.ClearLog();
        }

    }
}