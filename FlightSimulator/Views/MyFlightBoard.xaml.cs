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
        public MyFlightBoard(FlightBoardViewModel fbViewModel)
        {
            InitializeComponent();
            this.DataContext = fbViewModel;
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
