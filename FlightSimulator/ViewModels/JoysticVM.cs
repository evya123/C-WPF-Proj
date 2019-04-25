using System.Collections.Generic;

namespace FlightSimulator.ViewModels
{
    public class JoysticVM
    {
        private double aileron = 0;
        private double rudder = 0;
        private double thruttle = 0;
        private double elevator = 0;

        public double Aileron
        {
            set
            {
                List<string> arg = new List<string>();
                aileron = value;
                arg.Add("aileron");
                arg.Add(aileron.ToString());
                if (CommandSingleton.Instance.isConnected())
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
                arg.Add(thruttle.ToString());
                if (CommandSingleton.Instance.isConnected())
                    CommandSingleton.Instance.setInfo(arg);
            }
            get { return rudder; }
        }
        public double Thruttle
        {
            set
            {
                List<string> arg = new List<string>();
                thruttle = value;
                arg.Add("thruttle");
                arg.Add(thruttle.ToString());
                if (CommandSingleton.Instance.isConnected())
                    CommandSingleton.Instance.setInfo(arg);
            }
            get { return thruttle; }
        }
        public double Elevator
        {
            set
            {
                List<string> arg = new List<string>();
                elevator = value;
                arg.Add("elevator");
                arg.Add(elevator.ToString());
                if (CommandSingleton.Instance.isConnected())
                    CommandSingleton.Instance.setInfo(arg);
            }
            get { return elevator; }
        }
    }
}
