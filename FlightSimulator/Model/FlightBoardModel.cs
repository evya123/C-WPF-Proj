using System.ComponentModel;
using System.Net.Sockets;

namespace FlightSimulator.Model
{
    public abstract class FlightBoardModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected string _data;
        public string Data
        {
            get { return _data; }
            set { _data = value; NotifyPropertyChanged("Data"); }
        }
        public FlightBoardModel()
        {
        }

        protected void NotifyPropertyChanged(string v)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(v));
        }

        abstract protected void _infoChannel_PropertyChanged(object sender, PropertyChangedEventArgs e);

        abstract public void start(int port);

        abstract public void stop();
    }
}
