using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightSimulator.Model;
using FlightSimulator.Views;
using FlightSimulator.ViewModels;
using FlightSimulator.Properties;
using System.Windows.Input;


namespace FlightSimulator.ViewModels
{
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
                return localSettingsCommnad ?? (localSettingsCommnad= new CommandHandler(()=>OnClick()));
            
            }
        }

        private void OnClick()
        {
            Views.Settings s = new Views.Settings();
            s.ShowDialog();
        }




    }
}
