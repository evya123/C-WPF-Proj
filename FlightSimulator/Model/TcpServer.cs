using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
namespace FlightSimulator
{
    public delegate string DataHandler(string str);
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
                        Console.WriteLine("Client connected from " + tcpclient.Client.LocalEndPoint.ToString());
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
                                _myEvent.Invoke(request);
                            }
                        }
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