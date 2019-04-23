﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using FlightSimulator.Model;
namespace FlightSimulator.Views
{
    /// <summary>
    /// Interaction logic for MyFlightBoard.xaml
    /// </summary>
    public partial class MyFlightBoard : UserControl
    {

        public MyFlightBoard()
        {
            InitializeComponent();
            FlightBoardVMSingelton.Instance.PropertyChanged += FlightBoardResource.Vm_PropertyChanged;
            this.DataContext = FlightBoardVMSingelton.Instance;
        }


        private void FlightBoard_Loaded(object sender, RoutedEventArgs e)
        {
            //TODO
        }

        private void Connect_Click(object sender, RoutedEventArgs e)
        {
            Connect.IsEnabled = false;
        }

    }
}
