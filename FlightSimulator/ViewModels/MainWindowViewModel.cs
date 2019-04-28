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
                System.Windows.Application.Current.Exit += Current_Exit;
                System.Windows.Application.Current.Shutdown();
            }));
        }

        private void Current_Exit(object sender, System.Windows.ExitEventArgs e)
        {
            FlightBoardVMSingelton.Instance.Stop();
        }
    }
}
