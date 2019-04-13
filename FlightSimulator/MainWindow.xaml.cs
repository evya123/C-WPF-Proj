﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using FlightSimulator.Model;
using System.Threading;
using System.Net.Sockets;
namespace FlightSimulator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var asyncserver = new TcpServer();
            asyncserver.MyEvent += Asyncserver_MyEvent;
            asyncserver.Run(5400);
        }

        private void TabControl_Loaded(object sender, RoutedEventArgs e)
        {

        }


        private string Asyncserver_MyEvent(string str)
        {
            Console.WriteLine(str);
            return "";
        }
    }
}

