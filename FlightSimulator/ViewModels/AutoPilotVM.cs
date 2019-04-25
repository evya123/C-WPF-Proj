using System;
using System.Collections.Generic;
using System.Timers;
using System.Windows.Input;
using FlightSimulator.Model;


namespace FlightSimulator.ViewModels
{
    public class AutoPilotVM : BaseNotify
    {
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
            String[] allCommands = data.Split(new String[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            Queue<String> tokens = new Queue<string>();
            foreach (String token in allCommands)
                tokens.Enqueue(token);
            sendingData(tokens);
        }

        private void sendingData(Queue<String> tokens)
        {
            if(!CommandSingleton.Instance.isConnected())
                CommandSingleton.Instance.connectServer();
            var timer = new Timer(2000);
            timer.AutoReset = true;
            timer.Elapsed += (sender, e) => OnTimedEvent(sender, e, tokens);
            if (tokens.Count != 0)
                timer.Enabled = true;
        }

        private void OnTimedEvent(Object source, ElapsedEventArgs e, Queue<string> commands)
        {
            switch (commands.Count)
            {
                case 0:
                    Timer timer = (Timer)source; // Get the timer that fired the event
                    timer.Stop(); // Stop the timer that fired the event
                    break;
                default:
                    CommandSingleton.Instance.sendAutoData(commands.Dequeue());
                    break;
            }
        }
    }
}
