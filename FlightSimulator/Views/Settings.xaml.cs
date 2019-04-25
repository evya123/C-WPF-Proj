using System.Windows;

namespace FlightSimulator.Views
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        public Settings()
        {
            InitializeComponent();
            this.DataContext = MySettingVMSingelton.Instance;
            //MySettingVMSingelton.Instance.CloseRequested += vm_CloseRequested;

        }

       /* private void vm_CloseRequested(object sender, ViewModels.Windows.CloseRequestedEventArgs e)
        {
            if (e.DialogResult.HasValue)
                this.DialogResult = e.DialogResult; // sets the dialog result AND closes the window
            else
                this.Close();
        }*/

    }
}
