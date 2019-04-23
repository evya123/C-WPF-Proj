using FlightSimulator.Model.Interface;
using FlightSimulator.ViewModels.Windows;
using System;

namespace FlightSimulator.ViewModels
{
    public class CloseRequestedEventArgs : EventArgs
    {
        private readonly bool? _dialogResult;

        public CloseRequestedEventArgs(bool? dialogResult)
        {
            _dialogResult = dialogResult;
        }

        public bool? DialogResult => _dialogResult;

    }

    public class MySettingWindowVM : SettingsWindowViewModel
    {
        public event EventHandler<CloseRequestedEventArgs> CloseRequested;

        protected virtual void OnCloseRequested(bool? dialogResult)
        {
            var handler = CloseRequested;
            if (handler != null)
                handler(this, new CloseRequestedEventArgs(dialogResult));
        }

        public MySettingWindowVM(ISettingsModel model) : base(model)
        {
        }

    }
}
}
