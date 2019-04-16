using FlightSimulator.Model;
using System;
using System.Windows.Input;


namespace FlightSimulator.ViewModels
{
    public class FlightBoardViewModel : BaseNotify
    {
        private ICommand localSettingsCommnad;
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

        class SettingsVM
        {
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

            private void OnClick()
            {
                Views.Settings s = new Views.Settings();
                s.ShowDialog();
            }
        }
    }
}
