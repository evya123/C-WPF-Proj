using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator.ViewModels
{
    class JoysticVM
    {
        private double aileron = 0;
        private double rudder = 0;
        private double thruttle = 0;
        private double elevator = 0;

        public double Aileron
        {
            set
            {
                aileron = value;
            }
            get { return aileron; }
        }

        public double Rudder
        {
            set
            {
                rudder = value;
            }
            get { return rudder; }
        }
        public double Thruttle
        {
            set
            {
                thruttle = value;
            }
            get { return thruttle; }
        }

        public double Elevator
        {
            set
            {
                elevator = value;
            }
            get { return elevator; }
        }

}
}
