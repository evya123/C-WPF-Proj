using System;
using System.ComponentModel;
using System.Net.Sockets;

namespace FlightSimulator.Model
{
    class MyFlightBoardModel : FlightBoardModel
    {
        private TcpServer _infoChannel;

        public MyFlightBoardModel()
        {
            this._infoChannel = new TcpServer();
            this._infoChannel.PropertyChanged += _infoChannel_PropertyChanged;
        }

        protected override void _infoChannel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            _data = _infoChannel.Data;
        }

        public override void start(int port)
        {
            _infoChannel.Run(port);
        }

        public override void stop()
        {
            _infoChannel.Disconnect();
        }

    }
}
