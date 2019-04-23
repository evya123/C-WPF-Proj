﻿using FlightSimulator.Model;
using System;
using System.Windows.Input;

namespace FlightSimulator.ViewModels
{
    public class FlightBoardViewModel : BaseNotify
    {
        private CommandHandler _exitHandler;
        public CommandHandler ExitCommand
        {
            private set
            {

            }
            get => _exitHandler ?? (_exitHandler = new CommandHandler(() =>
            {
                Stop();
            }));
        }

        //###############//
        private ICommand _settingCommand;
        public ICommand SettingsCommand
        {
            get
            {
                return _settingCommand ?? (_settingCommand = new CommandHandler(() => SettingsClicked()));
            }
        }

        //###############//
        private ICommand _connectCommand;
        public ICommand ConnectCommand
        {
            get
            {
                return _connectCommand ?? (_connectCommand = new CommandHandler(() => ConnectClicked()));
            }
        }

        //###############//
        private void ConnectClicked()
        {
            InfoSingleton.Instance.Run(ApplicationSettingsModel.Instance.FlightInfoPort);
        }
        private void SettingsClicked()
        {
            Views.Settings set = new Views.Settings();
            set.ShowDialog();
        }

        //###############//
        private Double _lon;
        private Double _lat;
        public Double Lon
        {
            get { return _lon; }
            set { _lon = value; NotifyPropertyChanged("Lon"); }
        }
        public Double Lat
        {
            get { return _lat; }
            set { _lat = value; NotifyPropertyChanged("Lat"); }
        }

        //###############//
        public FlightBoardViewModel()
        {
            FlightBoardModelSingelton.Instance.PropertyChanged += _fbModel_PropertyChanged;
        }

        public void Start(int port)
        {
            FlightBoardModelSingelton.Instance.start(port);
        }

        public void Stop()
        {
            FlightBoardModelSingelton.Instance.stop();
        }

        protected void _fbModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            string rawData = FlightBoardModelSingelton.Instance.Data;
            string[] tokens = rawData.Split(',');
            try
            {
                Lon = Double.Parse(tokens[1]);
                Lat = Double.Parse(tokens[2]);
            } catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
            }
        }
    }
}
