﻿using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
namespace FlightSimulator
{
    public delegate void DataHandler(TcpClient tcp, NetworkStream netstream);

    public class TcpServer
    {
        private TcpClient tcpclient = null;
        private NetworkStream netstream = null;

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
                        Disconnect();
                        Console.WriteLine(ex.Message);
                    }
                    finally
                    {
                        Disconnect();
                    }
                }
            });
            thread.Start();
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