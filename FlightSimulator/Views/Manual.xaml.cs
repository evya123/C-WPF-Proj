using System.Windows.Controls;

namespace FlightSimulator.Views
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class Manual : UserControl
    {
        public Manual()
        {
            InitializeComponent();
            this.DataContext = JoystickVMSingelton.Instance;
        }  

    }
}
