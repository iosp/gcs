using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using Common.Enums;
using Common.Utils;
using GCSArduino.Interface;
using GCSArduino.Messengers;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Interfaces;
using Microsoft.Practices.ServiceLocation;

namespace GCSArduino.ViewModel
{
    public class ArduCopterModeViewModel :INotifyPropertyChanged//, IArduCopterMode
    {
        private Visibility _visibility;
        public RelayCommand<VehicleModeEnum> QuadModeCommand { get; set; }
        
        public IVehicleComponent VehicleComponent { get; set; }
        
        private IVehicle _vehicleSelected;
        public IVehicle VehicleSelected
        {
            get { return _vehicleSelected; }
            set { _vehicleSelected = value; OnPropertyChanged(); }
        }

        public ArduCopterModeViewModel(IVehicleComponent vehicleComponent)
        {
            VehicleComponent = vehicleComponent;
            QuadModeCommand = new RelayCommand<VehicleModeEnum>(ArduModeCommandExecute);
            Visibility = Visibility.Collapsed;
        }

        public void SetVehicleId(long id)
        {
            foreach (var vehicle in ServiceLocator.Current.GetInstance<IVehiclesSource>().Vehicles.Where(vehicle => vehicle.ID == id))
            {
                VehicleSelected = vehicle;
            }
        }


        private void ArduModeCommandExecute(VehicleModeEnum obj)
        {
            if (VehicleSelected != null)
                VehicleComponent.SendModeCommand(VehicleSelected.ID, (uint)obj);
        }

        public Visibility Visibility
        {
            get { return _visibility; }
            set { _visibility = value;OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
