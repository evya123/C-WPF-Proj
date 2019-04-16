using System;
using System.Windows;
using System.Net.Sockets;
using System.IO;
using System.Text;
using FlightSimulator.ViewModels;
using FlightSimulator.Model;

namespace FlightSimulator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private FlightBoardViewModel _fbViewModel;
        public MainWindow()
        {
            InitializeComponent();
            MyFlightBoardModel _mfbModel = new MyFlightBoardModel();
            this._fbViewModel = new FlightBoardViewModel(_mfbModel);
            this.DataContext = _fbViewModel;
        }

        private void TabControl_Loaded(object sender, RoutedEventArgs e)
        {

        }

    }
}

