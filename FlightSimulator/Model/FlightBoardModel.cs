using System.ComponentModel;

namespace FlightSimulator.Model
{
    public class FlightBoardModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected string _data;
        public string Data
        {
            get { return _data; }
            set { _data = value; NotifyPropertyChanged("Data"); }
        }
        protected void NotifyPropertyChanged(string v)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(v));
        }


        public FlightBoardModel()
        {
            InfoSingleton.Instance.PropertyChanged += _infoChannel_PropertyChanged;
        }

        protected void _infoChannel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            this.Data = InfoSingleton.Instance.Data;
        }

        public void start(int port)
        {
            InfoSingleton.Instance.RunCommand(port);
        }

        public void Stop()
        {
            InfoSingleton.Instance.Disconnect();
        }
    }
}
