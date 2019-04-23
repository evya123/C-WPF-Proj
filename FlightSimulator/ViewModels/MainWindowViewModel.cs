using FlightSimulator.ViewModels;
using System;

namespace FlightSimulator.Model
{
    public class MainWindowViewModel : BaseNotify
    {
        private CommandHandler _exitHandler;
        public CommandHandler ExitCommand
        {
            private set
            {

            }
            get => _exitHandler ?? (_exitHandler = new CommandHandler(() =>
            {
                FlightBoardVMSingelton.Instance.ExitCommand.Execute(null);
                System.Windows.Application.Current.Shutdown();
            }));
        }
    }
}
