using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator.Model
{
    class FlightBoardModel
    {
        private TcpServer _infoChannel;

        public FlightBoardModel(DataHandler dh)
        {
            this._infoChannel = new TcpServer();
            this._infoChannel.MyEvent += dh;
        }

        public void start(int port)
        {
            _infoChannel.Run(port);
        }

        public void stop()
        {
            _infoChannel.Disconnect();
        }
    }
}
