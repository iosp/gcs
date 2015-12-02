using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Common.Annotations;
using GCSArduino.Interface;
using GCSArduino.Messengers;
using GCSArduino.Model;
using GalaSoft.MvvmLight.Messaging;
using Interfaces;
using Microsoft.Practices.ServiceLocation;

namespace GCSArduino.ViewModel
{
    public class InstrumentControlViewModel : IInstrumentControl,INotifyPropertyChanged 
    {
        public IVehiclesSource VehiclesSource { get; set; }

        public InstrumentControlViewModel(IVehiclesSource vehiclesSource)
        {
            VehiclesSource = vehiclesSource;
            Messenger.Default.Register<VehicleSelected>(this, VehicleSelectedAction);

        }
        private IVehicle _vehicleSelected;
        public IVehicle VehicleSelected
        {
            get { return _vehicleSelected; }
            set { _vehicleSelected = value; OnPropertyChanged(); }
        }

        private void VehicleSelectedAction(VehicleSelected obj)
        {
            if (obj.Vehicle == null) return;
            VehicleSelected = obj.Vehicle;
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
