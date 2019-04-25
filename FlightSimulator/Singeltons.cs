
using FlightSimulator.ViewModels;
using FlightSimulator.Model;
using FlightSimulator.ViewModels.Windows;

namespace FlightSimulator
{
    public class InfoSingleton
    {
        private static TcpServer _instance = null;

        public static TcpServer Instance
        {
            private set { }
            get
            {
                if (_instance == null)
                    _instance = new TcpServer();
                return _instance;
            }
        }
    }

    public class CommandSingleton
    {
        private static Client _instance = null;

        public static Client Instance
        {
            private set { }
            get
            {
                if (_instance == null)
                    _instance = new Client();
                return _instance;
            }
        }
    }

    public class FlightBoardModelSingelton
    {
        private static FlightBoardModel _instance = null;

        public static FlightBoardModel Instance
        {
            private set { }
            get
            {
                if (_instance == null)
                    _instance = new FlightBoardModel();
                return _instance;
            }
        }
    }

    public class AutoPilotVMSingelton
    {
        private static AutoPilotVM _instance = null;

        public static AutoPilotVM Instance
        {
            private set { }
            get
            {
                if (_instance == null)
                    _instance = new AutoPilotVM();
                return _instance;
            }
        }
    }

    public class FlightBoardVMSingelton
    {
        private static FlightBoardViewModel _instance = null;

        public static FlightBoardViewModel Instance
        {
            private set { }
            get
            {
                if (_instance == null)
                    _instance = new FlightBoardViewModel();
                return _instance;
            }
        }
    }

    public class JoysticVMSingelton
    {
        private static JoysticVM _instance = null;

        public static JoysticVM Instance
        {
            private set { }
            get
            {
                if (_instance == null)
                    _instance = new JoysticVM();
                return _instance;
            }
        }
    }

    public class MainWindowVMSingelton
    {
        private static MainWindowViewModel _instance = null;

        public static MainWindowViewModel Instance
        {
            private set { }
            get
            {
                if (_instance == null)
                    _instance = new MainWindowViewModel();
                return _instance;
            }
        }
    }

    public class MySettingVMSingelton
    {
        private static SettingsWindowViewModel _instance = null;

        public static SettingsWindowViewModel Instance
        {
            private set { }
            get
            {
                if (_instance == null)
                    _instance = new SettingsWindowViewModel(ApplicationSettingsModel.Instance);
                return _instance;
            }
        }
    }

}
