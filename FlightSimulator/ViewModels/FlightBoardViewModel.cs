using FlightSimulator.Model;
using System;
using System.Net.Sockets;
using System.Windows.Input;

namespace FlightSimulator.ViewModels
{
    public class FlightBoardViewModel : BaseNotify
    {
        private readonly FlightBoardModel _fbModel;
        private ICommand localSettingsCommnad;
        public ICommand SettingsCommnad
        {
            set
            {

            }
            get
            {
                return localSettingsCommnad ?? (localSettingsCommnad = new CommandHandler(() => OnClick()));

            }
        }
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

        public FlightBoardViewModel()
        {
            this._fbModel = new MyFlightBoardModel();
            this._fbModel.PropertyChanged += _fbModel_PropertyChanged;
            this._fbModel.start(5400);
        }

        private void _fbModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            string rawData = _fbModel.Data;
            string[] tokens = rawData.Split(',');
            try
            {
                _lon = Double.Parse(tokens[0]);
                _lat = Double.Parse(tokens[1]);
            } catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
            }
        }

        private void OnClick()
        {
            Views.Settings s = new Views.Settings();
            s.ShowDialog();
        }
}
}
