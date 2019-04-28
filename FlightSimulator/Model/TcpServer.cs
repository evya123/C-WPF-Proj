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
        private int port;  
        private Thread thread = null;
        private string _data;
        //Notify about data from the simulator
        public string Data {
            get { return _data; }
            set { _data = value; NotifyPropertyChanged("Data"); } }

        public event PropertyChangedEventHandler PropertyChanged;
        //Main method for running server. start's listening and set the parameters for the thread.
        public void RunCommand(int port)
        {
            this.port = port;
            try
            {
                // open the thread that the commands will be on a different thread
                thread= new Thread(paradicat);
                thread.IsBackground = true;
                thread.Start();
            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        //Disconnecting from server
       /* public void Disconnect()
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
        }*/
        //Mathod for getting data from client
        private void paradicat(object obj)
        {
            TcpClient tcpclient = null;
            NetworkStream netstream = null;
            var listener = new TcpListener(IPAddress.Any, port);
            listener.Start();
            tcpclient = listener.AcceptTcpClient();
            netstream = tcpclient.GetStream();
            Console.WriteLine("The simulator is connected!");
            while (true)
            {
                try
                {
                    if (TcpHelper.GetState(tcpclient) == System.Net.NetworkInformation.TcpState.Closed)
                    {
                    Console.WriteLine("Client disconnected gracefully");
                    break;
                    }

                
                    Data = Read(netstream);
                }
                catch (ObjectDisposedException)
                {
                    Console.WriteLine("netstream has died");
                }
            }

            tcpclient.Close();
            netstream.Close();
            listener.Stop();
            tcpclient.Dispose();
            netstream.Dispose();
        }

        //check if the tcp connection is closed
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
        //Read data stream and decode is
        private string Read(NetworkStream netstream)
        {
            byte[] buffer = new byte[1024];
            int dataread = netstream.Read(buffer, 0, buffer.Length);
            string stringread = Encoding.UTF8.GetString(buffer, 0, dataread);
            return stringread;
        }
    }
}