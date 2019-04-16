using FlightSimulator.Model;
using System;

namespace FlightSimulator.ViewModels
{
    public class FlightBoardViewModel : BaseNotify
    {
        private FlightBoardModel _fbModel;
        private Double _lon;
        private Double _lat;
        public double Lon
        {
            get { return _lon; }
            set { _lon = value; NotifyPropertyChanged("Lon"); }
        }

        public double Lat
        {
            get { return _lat; }
            set { _lat = value; NotifyPropertyChanged("Lat"); }

        }

        public FlightBoardViewModel(DataHandler dh)
        {
            this._fbModel = new FlightBoardModel(dh);
            this._fbModel.start(5400);
        }
    }
}
