using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace FlightSimulator.Model
{
    public class Client
    {

        public Dictionary<string, double> pathRead = new Dictionary<string, double>();
        public Dictionary<string, string> SimulatorPath = new Dictionary<string, string>();

        private NetworkStream ns;
        private TcpClient client;
        private TcpListener server;

        public Client()
        {
            this.SimulatorPath.Add("aileron", "/controls/flight/aileron");
            this.SimulatorPath.Add("elevator", "/controls/flight/elevator");
            this.SimulatorPath.Add("rudder", "/controls/flight/rudder");
            this.SimulatorPath.Add("throttle", "/controls/engines/current-engine/throttle");
        }

        public Boolean isConnected()
        {
            try
            {
                return client.Connected;
            } catch (Exception e) {
                return false;
            }
        }

        public void connectServer()
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(Properties.Settings.Default.FlightServerIP),
                                                               Properties.Settings.Default.FlightCommandPort);
            this.server = new TcpListener(ep);
            this.client = new TcpClient();
            try
            {
                while (!client.Connected)
                {
                    Console.WriteLine("Sleeping!!!");
                    Thread.Sleep(2000);
                    client.Connect(ep);
                }
                Console.WriteLine("You are connected!");
                this.ns = client.GetStream();
            } catch (Exception e)
            {
                if (client != null)
                    client.Close();
                if (ns != null)
                    ns.Close();
                Console.WriteLine(e.Message);
            }
        }

        public void setInfo(List<string> path)
        {
            string command = "set ";
            command += this.SimulatorPath[path[0]] + " " + path[1] + "\r\n";
            Console.WriteLine(command);
            byte[] byteTime = Encoding.ASCII.GetBytes(command.ToString());
            this.ns.Write(byteTime, 0, byteTime.Length);

        }

        public void sendAutoData(String command)
        {
            if(command.Length != 0)
            {
                using (NetworkStream stream = new NetworkStream(this.client.Client, false))
                using (BinaryWriter writer = new BinaryWriter(stream))
                {
                    byte[] data = Encoding.ASCII.GetBytes(command);
                    Console.WriteLine(command);
                    writer.Write(data);
                    writer.Flush();
                }
            }
        }

        /*close the connection to the server*/
        public void close()
        {
            this.client.Close();
            this.server.Stop();
        }
    }
}
