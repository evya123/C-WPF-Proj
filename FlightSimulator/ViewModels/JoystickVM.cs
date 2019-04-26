using System;
using System.Collections.Generic;

namespace FlightSimulator.ViewModels
{
    public class JoystickVM
    {
        private double aileron = 0;
        private double rudder = 0;
        private double throttle = 0;
        private double elevator = 0;

        public double Aileron
        {
            set
            {
                List<string> arg = new List<string>();
                aileron = value;
                arg.Add("aileron");
                arg.Add(aileron.ToString());
                CommandSingleton.Instance.setInfo(arg);
            }
            get { return aileron; }
        }
        public double Rudder
        {
            set
            {
                List<string> arg = new List<string>();
                rudder = value;
                arg.Add("rudder");
                arg.Add(rudder.ToString());
                CommandSingleton.Instance.setInfo(arg);
            }
            get { return rudder; }
        }
        public double Throttle
        {
            set
            {
                List<string> arg = new List<string>();
                throttle = value;
                arg.Add("throttle");
                arg.Add(throttle.ToString());
                CommandSingleton.Instance.setInfo(arg);
            }
            get { return throttle; }
        }
        public double Elevator
        {
            set
            {
                List<string> arg = new List<string>();
                elevator = value;
                arg.Add("elevator");
                arg.Add(elevator.ToString());
                CommandSingleton.Instance.setInfo(arg);
            }
            get { return elevator; }
        }
    }
}
