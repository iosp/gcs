using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Threading;
using Common.Class;
using Common.Enums;
using GCSArduino.Annotations;
using GCSArduino.Enums;
using GCSArduino.Interface;
using GCSArduino.Messengers;
using GCSArduino.Model;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Interfaces;
using MMAVLink;
using Microsoft.Practices.ServiceLocation;
using MissionPackage;

namespace GCSArduino.ViewModel
{
    public class QuadPropertyViewModel : IQuadProperty, INotifyPropertyChanged
    {
        public RelayCommand<VehicleModeEnum> QuadModeCommand { get; set; }
        public IVehicleComponent VehicleComponent { get; set; }
        private bool _isJoystickChecked;
        public RelayCommand SetJoystickCommand { get; set; }
        public RelayCommand ChangeModeCommand { get; set; }
        public RelayCommand FlyCommand { get; set; }
        public RelayCommand<SoftkeyEnum> ButtonCommands { get; set; }
        public IMissionComponent MissionComponent { get; set; }
        private readonly DispatcherTimer _dispatcherUiTimer;
        private const int UpdataTimer = 100;
        const int DefaultAltitude = 100;

        public RelayCommand WriteMissionCommand { get; set; }
        public RelayCommand ReadMissionCommand { get; set; }
        public RelayCommand LoadMissionCommand { get; set; }
        public RelayCommand SaveMissionCommand { get; set; }

        public QuadPropertyViewModel(IToolAction toolAction , IMissionComponent missionComponent,IVehicleComponent vehicleComponent)
        {
            VehicleSelected = toolAction.VehicleSelected;
            MissionComponent = missionComponent;
            VehicleComponent = vehicleComponent;
            QuadModeCommand = new RelayCommand<VehicleModeEnum>(ArduModeCommandExecute);
            ChangeModeCommand = new RelayCommand(ChangeModeCommandExecute);
            ButtonCommands = new RelayCommand<SoftkeyEnum>(ButtonCommandsAction);
            FlyCommand = new RelayCommand(FlyCommandExecute);
            ModeControlVisibility = Visibility.Collapsed;

            WriteMissionCommand = new RelayCommand(WriteMissionCommandExecute, WriteMissionCommandCanExecute);
            ReadMissionCommand = new RelayCommand(ReadMissionCommandExecute, ReadMissionCommandCanExecute);
            LoadMissionCommand = new RelayCommand(LoadMissionCommandExecute, LoadMissionCommandCanExecute);
            SaveMissionCommand = new RelayCommand(SaveMissionCommandExecute, SaveMissionCommandCanExecute);

            Messenger.Default.Register<VehicleSelected>(this, VehicleSelectedAction);

            _dispatcherUiTimer = new DispatcherTimer { Interval = new TimeSpan(0, 0, 0, 0, UpdataTimer) };
            _dispatcherUiTimer.Tick += DispatcherUiTimerTick;
            _dispatcherUiTimer.Start();
        }
        private void VehicleSelectedAction(VehicleSelected obj)
        {
            if(obj.Vehicle == null )return;
            VehicleSelected = obj.Vehicle;
        }
        private void ArduModeCommandExecute(VehicleModeEnum obj)
        {
            if (VehicleSelected != null)
            {
                if (VehicleComponent == null)
                    VehicleComponent = ServiceLocator.Current.GetInstance<IVehicleComponent>();
                if (VehicleComponent != null)
                    VehicleComponent.SendModeCommand(VehicleSelected.ID, (uint)obj);
            }
        }
        private void DispatcherUiTimerTick(object sender, EventArgs e)
        {
            UpdateStatusBar();
        }

        private void UpdateStatusBar()
        {
            DateTime = DateTime.Now;
        }
        private void ButtonCommandsAction(SoftkeyEnum obj)
        {
            switch (obj)
            {
                case SoftkeyEnum.ArmDisArm:
                    ArmDisArm();
                    break;
                case SoftkeyEnum.Loiter:
                    VehicleComponent.SendModeCommand(VehicleSelected.ID, (uint)VehicleModeEnum.Loiter);
                    break;
                case SoftkeyEnum.ReturnToLaunch:
                    VehicleComponent.SendModeCommand(VehicleSelected.ID, (uint)VehicleModeEnum.RTL);
                    break;
                case SoftkeyEnum.SetHome:
                    ServiceLocator.Current.GetInstance<IVehiclesSource>().VehicleSelected.SetCurrentHome();
                    VehicleComponent.SetHomePositionCommand(VehicleSelected.ID, ServiceLocator.Current.GetInstance<IVehiclesSource>().VehicleSelected.HomePosition);
                    break;
                case SoftkeyEnum.StartMission:
                    StartMission();
                    break;
                case SoftkeyEnum.StopMission:
                    break;
                case SoftkeyEnum.RestartMission:
                    RestartMission();
                    break;
                case SoftkeyEnum.Auto:
                    VehicleComponent.SendModeCommand(VehicleSelected.ID, (uint)VehicleModeEnum.Auto);
                    break;
                case SoftkeyEnum.Sensor:
                    break;
                case SoftkeyEnum.TaskOff:
                    VehicleComponent.TakeOffCommand(VehicleSelected.ID, 100, (float)VehicleSelected.Latitude,
                                                     (float)VehicleSelected.Longitude, (float)VehicleSelected.Heading);


                    break;
                default:
                   break;
            }
        }

        private void RestartMission()
        {
            if (VehicleSelected != null)
                MissionComponent.RestartMission(VehicleSelected.ID, 0, 0);
        }


        public void ArmDisArm()
        {
            var vehicle = VehicleSelected as GuiQuadVehicle;
            if (vehicle != null)
                VehicleComponent.SetArmDisArmCommand(vehicle.ID, !vehicle.QuadStatus.Armed);
        }


        public void StartMission()
        {
            if (VehicleSelected != null)
                MissionComponent.StartMission(VehicleSelected.ID, 0);
        }

        public void GotoWaypoint(float Latitude, float longitude, float altitude, float param1, float param2, float param3, float angle)
        {
            if (VehicleSelected != null)
            {
                var gotoWaypoint = new MavlinkWaypoint
                {
                    Latitude = Latitude,
                    Longitude = longitude,
                    Altitude = altitude,
                    Id = 0,
                    Command = (ushort)MAVLink.MAV_CMD.OVERRIDE_GOTO,
                    Param1 = param1,
                    Param2 = param2,
                    Param3 = param3,
                    Param4 = angle,
                };

                VehicleComponent.GotoWaypointCommand(VehicleSelected.ID, gotoWaypoint);
            }
        }

        private void SaveMissionCommandExecute()
        {
            //ServiceLocator.Current.GetInstance<IMissionSource>().SaveMission();
        }

        private void LoadMissionCommandExecute()
        {
            //ServiceLocator.Current.GetInstance<IMissionSource>().LoadMission();
        }

        private void ReadMissionCommandExecute()
        {
            if (VehicleSelected != null)
                MissionComponent.ReadMission(VehicleSelected.ID);
        }

        private void WriteMissionCommandExecute()
        {
            if (VehicleSelected != null && VehicleSelected.Tasks.Count > 0)
                MissionComponent.UploadTask(VehicleSelected.ID, VehicleSelected.HomePosition,
                                            new MavlinkWaypoint
                                                {
                                                    Latitude = VehicleSelected.Latitude,
                                                    Longitude = VehicleSelected.Longitude,
                                                    Altitude = DefaultAltitude,
                                                    Param4 = (float) VehicleSelected.Heading
                                                },
                                            VehicleSelected.Tasks[0]);
        }

        private bool SaveMissionCommandCanExecute()
        {
            return true;
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

        private IVehicle _vehicleSelected;
        private Visibility _buttonsControlVisibility;
        private Visibility _modeControlVisibility;

        public IVehicle VehicleSelected
        {
            get { return _vehicleSelected; }
            set { _vehicleSelected = value; OnPropertyChanged();}
        }

       public Visibility ButtonsControlVisibility
       {
           get { return _buttonsControlVisibility; }
           set
           {
               if (value == _buttonsControlVisibility) return;
               _buttonsControlVisibility = value;
               OnPropertyChanged();
           }
       }

        public Visibility ModeControlVisibility
        {
            get { return _modeControlVisibility; }
            set
            {
                if (value == _modeControlVisibility) return;
                _modeControlVisibility = value;
                OnPropertyChanged();
            }
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
        private void ChangeModeCommandExecute()
        {
            ButtonsControlVisibility = Visibility.Collapsed;
            ModeControlVisibility = Visibility.Visible;
        }

        private void FlyCommandExecute()
        {
            ButtonsControlVisibility = Visibility.Visible;
            ModeControlVisibility = Visibility.Collapsed;
        }


        public bool IsJoystickChecked
        {
            get { return _isJoystickChecked; }
            set
            {
                _isJoystickChecked = value;
                if (value)
                {
                    VehicleComponent.SendModeCommand(VehicleSelected.ID, (uint) VehicleModeEnum.Loiter);
                    ServiceLocator.Current.GetInstance<IJoystick>().Start(VehicleSelected.ID);
                }
                else
                {
                    ServiceLocator.Current.GetInstance<IJoystick>().Stop();
                    VehicleComponent.SendModeCommand(VehicleSelected.ID, (uint)VehicleModeEnum.Auto);
                }
                OnPropertyChanged();
            }
        }
        //public MavlinkTaskNav NavTask
        //{
        //    get
        //    {
        //        //if (VehicleSelected.Tasks.Count > 0)
        //        return VehicleSelected.Waypoints ;
        //        return null;
        //    }
        //    //set
        //    //{
        //    //    if (Equals(value, _navTask)) return;
        //    //    _navTask = value;
        //    //    OnPropertyChanged();
        //    //}
        //}

        private Waypoint _selectedWaypoint;
        private MavlinkTaskNav _navTask;

        public Waypoint SelectedWaypoint
        {
            get { return _selectedWaypoint; }
            set
            {
                if (Equals(value, _selectedWaypoint)) return;
                _selectedWaypoint = value;
                OnPropertyChanged();
            }
        }
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
