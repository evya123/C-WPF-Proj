using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
namespace FlightSimulator
{
    public delegate void DataHandler(TcpClient tcp, NetworkStream netstream);
    public class TcpServer
    {
        private DataHandler _myEvent;

        public event DataHandler MyEvent
        {
            add
            {
                lock (this)
                {
                    _myEvent += value;
                }
            }
            remove
            {
                lock (this)
                {
                    _myEvent -= value;
                }
            }
        }

        public void Run(int port)
        {
            var listener = new TcpListener(IPAddress.Any, port);
            Console.WriteLine("Waiting for connection.....");
            listener.Start();
            Thread thread = new Thread(() => {
                while (true)
                {
                    TcpClient tcpclient = null;
                    NetworkStream netstream = null;
                    try
                    {
                        tcpclient = listener.AcceptTcpClient();
                        if (this._myEvent != null)
                            this._myEvent.Invoke(tcpclient, netstream);
                        else
                            throw this.NotImplementedException();
                    }
                    catch (Exception ex)
                    {
                        netstream.Close();
                        tcpclient.Close();
                        Console.WriteLine(ex.Message);
                    }
                    finally
                    {
                        netstream.Close();
                        tcpclient.Close();
                    }
                }
            });
            thread.Start();
        }

        private Exception NotImplementedException()
        {
            throw new NotImplementedException();
        }
    }
}