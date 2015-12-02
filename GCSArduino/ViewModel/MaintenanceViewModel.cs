using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Utils;
using GCSArduino.Interface;
using Interfaces;

namespace GCSArduino.ViewModel
{
    public class MaintenanceViewModel : IMaintenance
    {
        #region private Property
        private IVehiclesSource VehiclesSource { get; set; }
        private IMissionSource MissionSource { get; set; }
        private IVehicleComponent VehicleComponent { get; set; }
        private IMissionComponent MissionComponent { get; set; }
        #endregion
 
        public MaintenanceViewModel(IVehiclesSource vehiclesSource, IVehicleComponent vehicleComponent, IMissionComponent missionComponent, IMissionSource missionSource)
        {
            MissionSource = missionSource;
            MissionComponent = missionComponent;
            VehiclesSource = vehiclesSource;
            VehicleComponent = vehicleComponent;
        }

        #region Property
        public ObservableCollection<IVehicle> Vehicles
        {
            get { return VehiclesSource.Vehicles; }
        }
        #endregion
    }
}
