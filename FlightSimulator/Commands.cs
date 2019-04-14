using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator
{
    class Commands
    {
        public Dictionary<string, double> pathData = new Dictionary<string, double>();
        public Dictionary<string, string> SimulatorPathData = new Dictionary<string, string>();
        public Commands()
        {
            SetSimulatorPathData();
        }


        private void SetSimulatorPathData()
        {
            this.SimulatorPathData.Add("aileron", "/controls/flight/aileron");
            this.SimulatorPathData.Add("elevator", "/controls/flight/elevator");
            this.SimulatorPathData.Add("rudder", "/controls/flight/rudder");
            this.SimulatorPathData.Add("throttle", "/controls/engines/current-engine/throttle");
        }

        public void setPathData()
        {
            this.pathData.Add("/instrumentation/airspeed-indicator/indicated-speed-kt", 0);
            this.pathData.Add("/instrumentation/altimeter/indicated-altitude-ft", 0);
            this.pathData.Add("/instrumentation/altimeter/pressure-alt-ft", 0);
            this.pathData.Add("/instrumentation/attitude-indicator/indicated-pitch-deg", 0);
            this.pathData.Add("/instrumentation/attitude-indicator/indicated-roll-deg", 0);
            this.pathData.Add("/instrumentation/attitude-indicator/internal-pitch-deg", 0);
            this.pathData.Add("/instrumentation/attitude-indicator/internal-roll-deg", 0);
            this.pathData.Add("/instrumentation/encoder/indicated-altitude-ft", 0);
            this.pathData.Add("/instrumentation/encoder/pressure-alt-ft", 0);
            this.pathData.Add("/instrumentation/gps/indicated-altitude-ft", 0);
            this.pathData.Add("/instrumentation/gps/indicated-ground-speed-kt", 0);
            this.pathData.Add("/instrumentation/gps/indicated-vertical-speed", 0);
            this.pathData.Add("/instrumentation/heading-indicator/indicated-heading-deg", 0);
            this.pathData.Add("/instrumentation/magnetic-compass/indicated-heading-deg", 0);
            this.pathData.Add("/instrumentation/slip-skid-ball/indicated-slip-skid", 0);
            this.pathData.Add("/instrumentation/turn-indicator/indicated-turn-rate", 0);
            this.pathData.Add("/instrumentation/vertical-speed-indicator/indicated-speed-fpm", 0);
            this.pathData.Add("/controls/flight/aileron", 0);
            this.pathData.Add("/controls/flight/elevator", 0);
            this.pathData.Add("/controls/flight/rudder", 0);
            this.pathData.Add("/controls/flight/flaps", 0);
            this.pathData.Add("/controls/engines/current-engine/throttle", 0);
            this.pathData.Add("/engines/engine/rpm", 0);
        }

        public void setDict(List<double> dataVec)
        {
            this.pathData["/instrumentation/airspeed-indicator/indicated-speed-kt"] = dataVec[0];
            this.pathData["/instrumentation/altimeter/indicated-altitude-ft"] = dataVec[1];
            this.pathData["/instrumentation/altimeter/pressure-alt-ft"] = dataVec[2];
            this.pathData["/instrumentation/attitude-indicator/indicated-pitch-deg"] = dataVec[3];
            this.pathData["/instrumentation/attitude-indicator/indicated-roll-deg"] = dataVec[4];
            this.pathData["/instrumentation/attitude-indicator/internal-pitch-deg"] = dataVec[5];
            this.pathData["/instrumentation/attitude-indicator/internal-roll-deg"] = dataVec[6];
            this.pathData["/instrumentation/encoder/indicated-altitude-ft"] = dataVec[7];
            this.pathData["/instrumentation/encoder/pressure-alt-ft"] = dataVec[8];
            this.pathData["/instrumentation/gps/indicated-altitude-ft"] = dataVec[9];
            this.pathData["/instrumentation/gps/indicated-ground-speed-kt"] = dataVec[10];
            this.pathData["/instrumentation/gps/indicated-vertical-speed"] = dataVec[11];
            this.pathData["/instrumentation/heading-indicator/indicated-heading-deg"] = dataVec[12];
            this.pathData["/instrumentation/magnetic-compass/indicated-heading-deg"] = dataVec[13];
            this.pathData["/instrumentation/slip-skid-ball/indicated-slip-skid"] = dataVec[14];
            this.pathData["/instrumentation/turn-indicator/indicated-turn-rate"] = dataVec[15];
            this.pathData["/instrumentation/vertical-speed-indicator/indicated-speed-fpm"] = dataVec[16];
            this.pathData["/controls/flight/aileron"] = dataVec[17];
            this.pathData["/controls/flight/elevator"] = dataVec[18];
            this.pathData["/controls/flight/rudder"] = dataVec[19];
            this.pathData["/controls/flight/flaps"] = dataVec[20];
            this.pathData["/controls/engines/current-engine/throttle"] = dataVec[21];
            this.pathData["/engines/engine/rpm"] = dataVec[22];
        }
    }
}
