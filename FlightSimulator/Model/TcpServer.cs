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
        private TcpClient tcpclient = null;
        private NetworkStream netstream = null;
        private TcpListener listener = null;
        private CancellationTokenSource cts = null;
        private Thread thread = null;
        private string _data;

        public string Data {
            get { return _data; }
            set { _data = value; NotifyPropertyChanged("Data"); } }

        public event PropertyChangedEventHandler PropertyChanged;

        public void RunCommand(int port)
        {
            listener = new TcpListener(IPAddress.Any, port);
            cts = new CancellationTokenSource();
            try
            {
                thread= new Thread(new ParameterizedThreadStart(paradicat));
                thread.Start(cts.Token);
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
            {
                netstream.Close();
                netstream.Dispose();
            }
            if (tcpclient != null)
            {
                tcpclient.Close();
                tcpclient.Dispose();
            }
            cts.Dispose();
        }

        private void paradicat(object obj)
        {
            listener.Start();
            tcpclient = listener.AcceptTcpClient();
            netstream = tcpclient.GetStream();
            Console.WriteLine("The simulator is connected!");
            var responsewriter = new StreamWriter(netstream) { AutoFlush = true };
            while (true)
            {
                if (IsDisconnected(tcpclient))
                {
                    Disconnect();
//                    if (TcpHelper.GetState(this.tcpclient) == System.Net.NetworkInformation.TcpState.Closed)
                        cts.Cancel();
                    Console.WriteLine("Client disconnected gracefully");
                    break;
                }
                if (netstream.DataAvailable)             // handle scenario where client is not done yet, and DataAvailable is false. This is not part of the tcp protocol.
                {
                    Data = Read(netstream);
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

        private void NotifyPropertyChanged(string v)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(v));
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