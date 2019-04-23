﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using FlightSimulator.Model;


namespace FlightSimulator.ViewModels
{
    public class AutoPilotVM : BaseNotify
    {
        private String[] allCommands;
        private ICommand _okC;
        private ICommand _clear;
        // check if the user write or not in the textbox
        private bool isWrite =false;
        private String color;
        private String data = "";
        private String blank = "";
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

        public ICommand clearCommand
        {
            get
            {
                return _clear ?? (_clear = new CommandHandler(() => clearTextbox()));
            }
        }
        public void clearTextbox()
        {
            CommandsFromUser = "";
            isWrite = false;       
        }
        public ICommand OkCommand
        {
            get
            {
                return _okC ?? (_okC = new CommandHandler(() => parseCommands()));
            }
        }
        private void parseCommands()
        {
            string[] delimiter = { "\r\n" };
            allCommands = data.Split(delimiter, StringSplitOptions.None);
        }
    }
}
