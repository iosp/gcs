using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Threading;
using Common.Annotations;
using Common.Enums;
using GCSArduino.Enums;
using GCSArduino.Interface;
using GCSArduino.Messengers;
using GCSArduino.Model;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Interfaces;
using Microsoft.Practices.ServiceLocation;

namespace GCSArduino.ViewModel
{
    public class QuadVideoViewModel :INotifyPropertyChanged//, IQuadVideo
    {
        public ObservableCollection<ErrorListModel> ErrorList
        {
            get { return _errorList; }
            set
            {
                if (Equals(value, _errorList)) return;
                _errorList = value;
                OnPropertyChanged();
            }
        }

        public IMissionSource MissionSource  { get; set; }
        public IVehicleComponent VehicleComponent { get; set; }
        public IMissionComponent MissionComponent { get; set; }

        private readonly DispatcherTimer _dispatcherUiTimer;
        private const int UpdataTimer = 100;

        public RelayCommand<VehicleModeEnum> QuadModeCommand { get; set; }
        public RelayCommand<SoftkeyEnum> ButtonCommands { get; set; }
        public RelayCommand WriteMissionCommand { get; set; }
        public RelayCommand ReadMissionCommand { get; set; }

        public QuadVideoViewModel(IMissionSource missionSource, IVehicleComponent vehicleComponent,  IMissionComponent missionComponent )
        {
            VehicleComponent = vehicleComponent;
            MissionComponent = missionComponent;
            MissionSource = missionSource;

            QuadModeCommand = new RelayCommand<VehicleModeEnum>(ArduModeCommandExecute);
            ButtonCommands = new RelayCommand<SoftkeyEnum>(ButtonCommandsAction);
            WriteMissionCommand = new RelayCommand(WriteMissionCommandExecute, WriteMissionCommandCanExecute);
            ReadMissionCommand = new RelayCommand(ReadMissionCommandExecute, ReadMissionCommandCanExecute);


            _dispatcherUiTimer = new DispatcherTimer { Interval = new TimeSpan(0, 0, 0, 0, UpdataTimer) };
            _dispatcherUiTimer.Tick += DispatcherUiTimerTick;
            _dispatcherUiTimer.Start();
        }

        private void ButtonCommandsAction(SoftkeyEnum obj)
        {
            //ServiceLocator.Current.GetInstance<IButtonsControl>().ButtonCommands.Execute(obj);
        }

        private void ArduModeCommandExecute(VehicleModeEnum obj)
        {
            if (VehicleSelected != null)
                VehicleComponent.SendModeCommand(VehicleSelected.ID, (uint)obj);
        }

        private void DispatcherUiTimerTick(object sender, EventArgs e)
        {
            UpdateStatusBar();
        }

        private void UpdateStatusBar()
        {
            DateTime = DateTime.Now;
        }
        public void SetVehicleId(long id)
        {
            foreach (var vehicle in ServiceLocator.Current.GetInstance<IVehiclesSource>().Vehicles.Where(vehicle => vehicle.ID == id))
            {
                VehicleSelected = vehicle;
            }
        }
        //private void VehicleSelectedAction(VehicleSelected obj)
        //{
        //    foreach (var vehicle in ServiceLocator.Current.GetInstance<IVehiclesSource>().Vehicles.Where(vehicle => vehicle.ID == obj.VehicleId))
        //    {
        //        VehicleSelected = vehicle;
        //    }
        //}

        private IVehicle _vehicleSelected;
        private ObservableCollection<ErrorListModel> _errorList;

        public IVehicle VehicleSelected
        {
            get { return _vehicleSelected; }
            set { _vehicleSelected = value; OnPropertyChanged(); }
        }

        private DateTime _dateTime;
        public DateTime DateTime
        {
            get { return _dateTime; }
            set
            {
                if (value.Equals(_dateTime)) return;
                _dateTime = value;
                OnPropertyChanged();
            }
        }
        private bool _isJoystickChecked;

        public bool IsJoystickChecked
        {
            get { return _isJoystickChecked; }
            set
            {
                _isJoystickChecked = value;
                if (value)
                    ServiceLocator.Current.GetInstance<IJoystick>().Start(VehicleSelected.ID);
                else
                    ServiceLocator.Current.GetInstance<IJoystick>().Stop();
                OnPropertyChanged();
            }
        }
        private void ReadMissionCommandExecute()
        {
            //ServiceLocator.Current.GetInstance<IListWaypoint>().DeleteWaypointCommand.Execute(null);
            if (VehicleSelected != null)
                MissionComponent.ReadMission(VehicleSelected.ID);
        }
        private void WriteMissionCommandExecute()
        {
            var mission = (MissionPackage.Mission)ServiceLocator.Current.GetInstance<IMissionSource>().Mission;
            if (VehicleSelected == null) return;
            mission.HomePosition = VehicleComponent.Vehicles.VehicleList[VehicleSelected.ID].HomePosition;

          
        }

        private bool WriteMissionCommandCanExecute()
        {
            return true;
        }
        private bool ReadMissionCommandCanExecute()
        {
            return true;
        }


        ~QuadVideoViewModel()
        {
            _dispatcherUiTimer.Tick -= DispatcherUiTimerTick;
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
