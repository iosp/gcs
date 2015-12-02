using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Common.Annotations;
using Common.Class;
using GCSArduino.Interface;
using GCSArduino.Messengers;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Interfaces;
using Microsoft.Practices.ServiceLocation;


namespace GCSArduino.ViewModel
{
    public class RightCommandsControlViewModel : INotifyPropertyChanged //, IRightCommandsControl 
    {
        private IVehicle _vehicleSelected;
        public RelayCommand WriteMissionCommand { get;set; }
        public RelayCommand ReadMissionCommand { get; set; }
        public RelayCommand LoadMissionCommand { get; set; }
        public RelayCommand SaveMissionCommand { get; set; }

        public RelayCommand DrawCommand { get; set; }

        public IVehicleComponent VehicleComponent { get; set; }
        public IMissionComponent MissionComponent { get; set; }
        public IMissionSource MissionSource { get; set; }
        //public IListWaypoint ListWaypoint { get; set; }

        public IVehicle VehicleSelected
        {
            get { return _vehicleSelected; }
            set { _vehicleSelected = value; OnPropertyChanged();}
        }

        //public Mission Mission { get; set; }
        public RightCommandsControlViewModel(IVehicleComponent vehicleComponent, IMissionComponent missionComponent,IMissionSource missionSource)
        {
            VehicleComponent = vehicleComponent;
            MissionComponent = missionComponent;
            MissionSource = missionSource;
            //ListWaypoint = listWaypoint;
            WriteMissionCommand = new RelayCommand(WriteMissionCommandExecute, WriteMissionCommandCanExecute);
            ReadMissionCommand = new RelayCommand(ReadMissionCommandExecute, ReadMissionCommandCanExecute);
            LoadMissionCommand = new RelayCommand(LoadMissionCommandExecute, LoadMissionCommandCanExecute);
            SaveMissionCommand = new RelayCommand(SaveMissionCommandExecute, SaveMissionCommandCanExecute);
            DrawCommand = new RelayCommand(DrawCommandExecute);
            Messenger.Default.Register<VehicleSelected>(this, VehicleSelectedAction);

        }

        private void VehicleSelectedAction(VehicleSelected obj)
        {
            if (obj.Vehicle == null) return;
            VehicleSelected = obj.Vehicle;
        }

        private bool SaveMissionCommandCanExecute()
        {
            return true;
        }

        private void SaveMissionCommandExecute()
        {
            ServiceLocator.Current.GetInstance<IMissionSource>().SaveMission();
        }

        private void LoadMissionCommandExecute()
        {
           ServiceLocator.Current.GetInstance<IMissionSource>().LoadMission();
        }

        private void ReadMissionCommandExecute()
        {
            //ServiceLocator.Current.GetInstance<IListWaypoint>().DeleteWaypointCommand.Execute(null);
            if (VehicleSelected != null)
            MissionComponent.ReadMission(VehicleSelected.ID);
        }

        private void DrawCommandExecute()
        {
        }

        private void WriteMissionCommandExecute()
        {
            var mission = (MissionPackage.Mission)ServiceLocator.Current.GetInstance<IMissionSource>().Mission;
            if (VehicleSelected == null)return;
            mission.HomePosition = VehicleComponent.Vehicles.VehicleList[VehicleSelected.ID].HomePosition;
        }

        private bool WriteMissionCommandCanExecute()
        {
            return true;
        }
        
        private bool LoadMissionCommandCanExecute()
        {
            return true;
        }

        private bool ReadMissionCommandCanExecute()
        {
            return true;
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
