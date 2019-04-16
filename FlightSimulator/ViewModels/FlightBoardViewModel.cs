using FlightSimulator.Model;

using System;
using System.Windows.Input;
using FlightSimulator.Views;

namespace FlightSimulator.ViewModels
{
    public class FlightBoardViewModel : BaseNotify
    {
        private FlightBoardModel _fbModel;
        private ICommand _set;
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

        public ICommand SettingsCommand
        {
            get
            {
                return _set ?? (_set = new CommandHandler(() => showSettings()));
            }
        }

        public void showSettings()
        {
            Settings s = new Settings();
            s.ShowDialog();
        }
    }
}
