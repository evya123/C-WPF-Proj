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
        private IPEndPoint ep;

        public Client()
        {
            ep = new IPEndPoint(IPAddress.Parse(Properties.Settings.Default.FlightServerIP),
                                                               Properties.Settings.Default.FlightCommandPort);
            this.client = new TcpClient();

            timer = new Timer(2000);
            timer.AutoReset = true;
            timer.Elapsed += (sender, e) => OnTimedEvent(sender, e, ep);

            this.SimulatorPath.Add("aileron", "/controls/flight/aileron");
            this.SimulatorPath.Add("elevator", "/controls/flight/elevator");
            this.SimulatorPath.Add("rudder", "/controls/flight/rudder");
            this.SimulatorPath.Add("throttle", "/controls/engines/current-engine/throttle");
        }
        //Timed event for try connecting to server. After connection is established, worker thread exits.
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
                } catch (System.Net.Sockets.SocketException ex) { };
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
            }
        }
        //firing the timer event
        public void Start() { this.timer.Start(); }
        //method for sending data from joystick
        public void setInfo(List<string> tokens)
        {
            if ((TcpHelper.GetState(this.client) != TcpState.Closed) &&
                (TcpHelper.GetState(this.client) != TcpState.Unknown))
            {
                string command = "set ";
                command += this.SimulatorPath[tokens[0]] + " " + tokens[1] + "\r\n";
                byte[] byteTime = Encoding.ASCII.GetBytes(command.ToString());
                this.ns.Write(byteTime, 0, byteTime.Length);
            }
        }
        //send ddata from autopilot
        public void sendAutoData(String command)
        {
            if ((TcpHelper.GetState(this.client) != TcpState.Closed) &&
                (TcpHelper.GetState(this.client) != TcpState.Unknown))
            {
                if (command.Length != 0)
                {
                    using (NetworkStream stream = new NetworkStream(this.client.Client, false))
                    using (BinaryWriter writer = new BinaryWriter(stream))
                    {
                        byte[] data = Encoding.ASCII.GetBytes((command+"\r\n"));
                        Console.WriteLine(command);
                        writer.Write(data);
                        writer.Flush();
                    }
                }
            }
        }

        /*close the connection to the server*/
        public void Stop()
        {
            if (TcpHelper.GetState(this.client) == TcpState.Established)
            {
                this.client.Close();
                this.client.Dispose();
            }
        }
    }
}
