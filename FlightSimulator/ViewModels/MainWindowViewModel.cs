using FlightSimulator.ViewModels;

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
                FlightBoardVMSingelton.Instance.Stop();
                System.Windows.Application.Current.Shutdown();
            }));
        }
    }
}
