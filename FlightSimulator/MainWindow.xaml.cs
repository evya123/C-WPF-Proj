using System;
using System.Windows;
using System.Net.Sockets;
using System.IO;
using System.Text;
using FlightSimulator.ViewModels;

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
            DataHandler dh = Asyncserver_MyEvent;
            this._fbViewModel = new FlightBoardViewModel(dh);
            this.DataContext = _fbViewModel;
        }

        private void TabControl_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Asyncserver_MyEvent(TcpClient tcpclient, NetworkStream netstream)
        {
            netstream = tcpclient.GetStream();
            var responsewriter = new StreamWriter(netstream) { AutoFlush = true };
            while (true)
            {
                if (IsDisconnected(tcpclient))
                {
                    Console.WriteLine("Client disconnected gracefully");
                    break;
                }
                if (netstream.DataAvailable)             // handle scenario where client is not done yet, and DataAvailable is false. This is not part of the tcp protocol.
                {
                    string request = Read(netstream);
                    string[] tokens = request.Split(',');
                    

                }
            }
        }

        private bool IsDisconnected(TcpClient tcp)
        {
            if (tcp.Client.Poll(0, SelectMode.SelectRead))
            {
                byte[] buff = new byte[1];
                if (tcp.Client.Receive(buff, SocketFlags.Peek) == 0)
                    return true;
            }
            return false;
        }

        private string Read(NetworkStream netstream)
        {
            byte[] buffer = new byte[1024];
            int dataread = netstream.Read(buffer, 0, buffer.Length);
            string stringread = Encoding.UTF8.GetString(buffer, 0, dataread);
            return stringread;
        }
    }
}

