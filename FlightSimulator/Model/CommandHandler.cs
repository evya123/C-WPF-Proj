using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Windows.Input;

namespace FlightSimulator.Model
{
    public class CommandHandler : ICommand
    {
        private Action _action;
        private Action<TcpClient, NetworkStream> a;

        public CommandHandler(Action action)
        {
            _action = action;
        }

        public CommandHandler(Action<TcpClient, NetworkStream> a)
        {
            this.a = a;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            _action();
        }
    }
}
