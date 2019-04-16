using System.Windows;
using System.Windows.Controls;
using FlightSimulator.ViewModels;
namespace FlightSimulator.Views
{
    /// <summary>
    /// Interaction logic for MyFlightBoard.xaml
    /// </summary>
    public partial class MyFlightBoard : UserControl
    {
        public MyFlightBoard()
        {
            InitializeComponent();
            FlightBoardViewModel vm = new FlightBoardViewModel();
            vm.PropertyChanged += FlightBoardResource.Vm_PropertyChanged;
            this.DataContext = vm;
        }

        private void ClickConnect(object sender, RoutedEventArgs e)
        {
            //TODO
        }

        private void ClickSettings(object sender, RoutedEventArgs e)
        {
            //TODO
        }

        private void FlightBoard_Loaded(object sender, RoutedEventArgs e)
        {
            //TODO
        }
    }
}
