using System;
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
            if (_action != null)
                return true;
            return false;
        }

        public bool CanExecute(object parameter1, object parameter2)
        {
            if (_action != null)
                return true;
            return false;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            _action();
        }

        public void Execute(object parameter1, object parameter2)
        {
            a((TcpClient)parameter1, (NetworkStream)parameter2);
        }
    }
}
