using FlightSimulator.Model;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
namespace FlightSimulator
{
    //public delegate void DataHandler(TcpClient tcp, NetworkStream netstream);

    public class TcpServer
    {
        private TcpClient tcpclient = null;
        private NetworkStream netstream = null;

        //private DataHandler _myEvent;
        /*public event DataHandler MyEvent
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
        }*/
        private CommandHandler _myHandler;

        public TcpServer(CommandHandler ch) { this._myHandler = ch; }
        public void Run(int port)
        {
            var listener = new TcpListener(IPAddress.Any, port);
            Console.WriteLine("Waiting for connection.....");
            try
            {
                listener.Start();
                Thread thread = new Thread(() => {
                    while (true)
                    {
                        tcpclient = listener.AcceptTcpClient();
                        if (this._myHandler != null)
                            this._myHandler.Execute(this);
                        else
                            throw this.NotImplementedException();
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

        private Exception NotImplementedException()
        {
            throw new NotImplementedException();
        }

        public void Disconnect()
        {
            netstream.Close();
            tcpclient.Close();
        }
    }
}