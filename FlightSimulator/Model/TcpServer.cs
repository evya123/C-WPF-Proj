using FlightSimulator.Model;
using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
namespace FlightSimulator
{
    public class TcpServer : INotifyPropertyChanged
    {
        TcpClient tcpclient = null;
        NetworkStream netstream = null;
        private string _data;
        public string Data {
            get { return _data; }
            set { _data = value; NotifyPropertyChanged("Data"); } }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string v)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(v));
        }

        public void Run(int port)
        {
            var listener = new TcpListener(IPAddress.Any, port);
            try
            {
                Thread thread = new Thread(() => {
                    listener.Start();
                    tcpclient = listener.AcceptTcpClient();
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
                            Data = Read(netstream);
                        }
                    }
                });
                thread.Start();
            } catch (Exception e)
            {
                Disconnect();
                Console.WriteLine(e.Message);
            }
            finally
            {
                Disconnect();
            }
        }

        public void Disconnect()
        {
            if (netstream != null)
                netstream.Close();
            if (tcpclient != null)
                tcpclient.Close();
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