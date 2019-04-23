﻿using System.Windows;
using System.Windows.Forms;

namespace FlightSimulator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        private void TabControl_Loaded(object sender, RoutedEventArgs e)
        {

        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            const string message = "Are you sure that you would like to close the form?" ;
            const string caption = "Form Closing";
            DialogResult result = System.Windows.Forms.MessageBox.Show(message, caption,
                                         MessageBoxButtons.YesNo,
                                         MessageBoxIcon.Question);

            // If the no button was pressed ...
            if (result == System.Windows.Forms.DialogResult.No)
            {
                // cancel the closure of the form.
                e.Cancel = true;
            } else
                MainWindowVMSingelton.Instance.ExitCommand.Execute(null);

        }
    }
}

