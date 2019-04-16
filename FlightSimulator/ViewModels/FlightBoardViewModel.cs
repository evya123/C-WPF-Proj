using FlightSimulator.Model;
using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Windows.Input;

namespace FlightSimulator.ViewModels
{
    public class FlightBoardViewModel : BaseNotify
    {
        private ICommand localSettingsCommnad;
        private FlightBoardModel _fbModel;
        private Double _lon;
        private Double _lat;
        public double Lon
        {
            get { return _lon; }
            set { _lon = value; NotifyPropertyChanged("Lon"); }
        }

        public double Lat
        {
            get { return _lat; }
            set { _lat = value; NotifyPropertyChanged("Lat"); }

        }

        public FlightBoardViewModel()
        {
            Action<TcpClient, NetworkStream> a = Asyncserver_MyEvent;
            CommandHandler ch = new CommandHandler(a);
            this._fbModel = new FlightBoardModel(ch);
            this._fbModel.start(5400);
        }

        class SettingsVM
        {
            private ICommand localSettingsCommnad;
            public ICommand SettingsCommnad
            {
                set
                {

                }
                get
                {
                    return localSettingsCommnad ?? (localSettingsCommnad = new CommandHandler(() => OnClick()));

                }
            }

            private void OnClick()
            {
                Views.Settings s = new Views.Settings();
                s.ShowDialog();
            }
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
                    foreach (var token in tokens)
                    {
                        Console.WriteLine(token);
                    }


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
