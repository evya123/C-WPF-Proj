using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Timers;

namespace FlightSimulator.Model
{
    public class Client
    {

        public Dictionary<string, double> pathRead = new Dictionary<string, double>();
        public Dictionary<string, string> SimulatorPath = new Dictionary<string, string>();

        private Timer timer;
        private NetworkStream ns;
        private TcpClient client;
        private TcpListener server;
        private IPEndPoint ep;

        public Client()
        {
            ep = new IPEndPoint(IPAddress.Parse(Properties.Settings.Default.FlightServerIP),
                                                               Properties.Settings.Default.FlightCommandPort);
            this.server = new TcpListener(ep);
            this.client = new TcpClient();

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
#pragma warning disable CS0168 // The variable 'e' is declared but never used
            } catch (Exception e) {
#pragma warning restore CS0168 // The variable 'e' is declared but never used
                return false;
            }
        }

        public void setServerEvent()
        {
                try
                {      
                    timer = new Timer(2000);
                    timer.AutoReset = true;
                    timer.Elapsed += (sender, e) => OnTimedEvent(sender, e, ep);
                }
                catch (Exception e)
                {
                    if (client != null)
                        client.Close();
                    if (ns != null)
                        ns.Close();
                    Console.WriteLine(e.Message);
                }
        }


        private void OnTimedEvent(object sender, ElapsedEventArgs e, IPEndPoint ep)
        {
            if ((TcpHelper.GetState(this.client) != TcpState.Closed) &&
                (TcpHelper.GetState(this.client) != TcpState.Unknown))
            {
                Console.WriteLine("You are connected!");
                this.ns = client.GetStream();
                Timer timer = (Timer)sender; // Get the timer that fired the event
                timer.Stop(); // Stop the timer that fired the event
            } else
            {
                Console.WriteLine("Trying to connect...");
                try
                {
                    client.Connect(ep);
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
                } catch (Exception ex) { };
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
            }
        }

        public void setInfo(List<string> tokens)
        {
            
            if ((TcpHelper.GetState(this.client) != TcpState.Closed) &&
                (TcpHelper.GetState(this.client) != TcpState.Unknown))
            {
                Console.WriteLine("State is: "+TcpHelper.GetState(this.client).ToString());
                string command = "set ";
                command += this.SimulatorPath[tokens[0]] + " " + tokens[1] + "\r\n";
                Console.WriteLine(command);
                byte[] byteTime = Encoding.ASCII.GetBytes(command.ToString());
                this.ns.Write(byteTime, 0, byteTime.Length);
            } else
            {
                CommandSingleton.Instance.setServerEvent();
                timer.Start();
            }
        }

        public void sendAutoData(String command)
        {
            CommandSingleton.Instance.setServerEvent();
            if ((TcpHelper.GetState(this.client) != TcpState.Closed) &&
                (TcpHelper.GetState(this.client) != TcpState.Unknown))
            {
                if (command.Length != 0)
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
            } else
            {
                CommandSingleton.Instance.setServerEvent();
                timer.Start();
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
