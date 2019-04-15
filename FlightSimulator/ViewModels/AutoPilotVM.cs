using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using FlightSimulator.Model;


namespace FlightSimulator.ViewModels
{
    class AutoPilotVM : BaseNotify
    {
        private List<List<string>> AllCommands = new List<List<string>>();
        private ICommand _connect;
        private ICommand _clear;
        // check if the user write or not in the textbox
        private bool isWrite =false;
        private string color;
        private String data = "";
        private string blank = "";
        public String ChangeColor
        {
            get
            {
                if (blank != "") 
                {
                    color = "Pink";
                }
                else
                {
                    color = "White";
                }
                return color;
            }
        }
        public String CommandsFromUser
        {
            set
            {
                this.data = value;
                blank = value;
                isWrite = true;
                NotifyPropertyChanged("CommandsFromUser");
                NotifyPropertyChanged("ChangeColor");

            }
            get
            {
                if (!isWrite)
                {
                    NotifyPropertyChanged("ChangeColor");
                }
                return data;
            }
            
        }

        public void OnClick()
        {
            blank = "";
        }

        public ICommand ClearCommand
        {
            get
            {
                return _clear ?? (_clear = new CommandHandler(() => clearTextbox()));
            }
        }
        public void clearTextbox()
        {
            isWrite = false;
            data = "";
            blank = "";
            NotifyPropertyChanged("CommandsFromUser");
            NotifyPropertyChanged("ChangeColor");
       
        }




    }
}
