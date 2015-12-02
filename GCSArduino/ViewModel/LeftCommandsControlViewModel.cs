using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using Common.Annotations;
using Common.Utils;
using GCSArduino.Interface;
using GalaSoft.MvvmLight.Command;
using Interfaces;
using Microsoft.Practices.ServiceLocation;

namespace GCSArduino.ViewModel
{
    public class LeftCommandsControlViewModel :INotifyPropertyChanged//, ILeftCommandsControl
    {
        private bool _isJoystickChecked;
        public RelayCommand SetJoystickCommand { get; set; }
        public RelayCommand ChangeModeCommand { get; set; }
        public RelayCommand FlyCommand { get; set; }

        public IVehicleComponent VehicleComponent { get; set; }
        public LeftCommandsControlViewModel(IVehicleComponent vehicleComponent)
        {
           
            ChangeModeCommand = new RelayCommand(ChangeModeCommandExecute);
            FlyCommand = new RelayCommand(FlyCommandExecute);
            VehicleComponent = vehicleComponent;
        }

        private void ChangeModeCommandExecute()
        {
            //ServiceLocator.Current.GetInstance<IButtonsControl>().Visibility = Visibility.Collapsed;
            //ServiceLocator.Current.GetInstance<IArduCopterMode>().Visibility = Visibility.Visible;
        }

        private void FlyCommandExecute()
        {
            //ServiceLocator.Current.GetInstance<IButtonsControl>().Visibility = Visibility.Visible;
            //ServiceLocator.Current.GetInstance<IArduCopterMode>().Visibility = Visibility.Collapsed;
        }


        public bool IsJoystickChecked
        {
            get { return _isJoystickChecked; }
            set
            {
                _isJoystickChecked = value;
                //if (value)
                //    ServiceLocator.Current.GetInstance<IJoystick>(idx).Start();
                //else
                //    ServiceLocator.Current.GetInstance<IJoystick>().Stop();
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
