using System.Windows;
using System.Windows.Controls;

namespace FlightSimulator.Views
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class AutoPilot : UserControl
    {
        public AutoPilot()
        {
            InitializeComponent();
            this.DataContext = AutoPilotVMSingelton.Instance;
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            //Todo

        }

        private void CLEAR_Click(object sender, RoutedEventArgs e)
        {
            //Todo
        }
    }
}
