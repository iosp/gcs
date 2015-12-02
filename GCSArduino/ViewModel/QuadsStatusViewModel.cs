using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using Common.AsyncFunction;
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
using MissionPackage;

namespace GCSArduino.ViewModel
{
    public class QuadsStatusViewModel : INotifyPropertyChanged, IQuadsStatus
    {
        #region private Property
        private IVehiclesSource VehiclesSource { get; set; }
        private IMissionSource MissionSource { get; set; }
        private IVehicleComponent VehicleComponent { get; set; }
        private IMissionComponent MissionComponent { get; set; }
        #endregion
 
        #region RelayCommand
        public RelayCommand ArmModeCommand { get; set; }
        public RelayCommand DisArmModeCommand { get; set; }
        public RelayCommand<VehicleModeEnum> QuadModeCommand { get; set; }
        public RelayCommand TakeOffCommand { get; set; }
        public RelayCommand<SoftkeyEnum> MissionCommands { get; set; }
        public RelayCommand JoystickRestartCommands { get; set; }
        public RelayCommand SetNextWaypointCommand { get; set; }
        public RelayCommand<object> SelectTaskCommand { get; set; }
        public RelayCommand SendMissionCommands { get; set; }
        #endregion

        #region Const
        const int DefaultAltitude = 100;
        #endregion

        #region Ctor
        
        public QuadsStatusViewModel(IVehiclesSource vehiclesSource, IVehicleComponent vehicleComponent, IMissionComponent missionComponent, IMissionSource missionSource)
        {
            MissionSource = missionSource;
            MissionComponent = missionComponent;
            VehiclesSource = vehiclesSource;
            VehicleComponent = vehicleComponent;
            ArmModeCommand = new RelayCommand(ArmModeCommandExecute);
            DisArmModeCommand = new RelayCommand(DisArmModeCommandExecute);
            QuadModeCommand = new RelayCommand<VehicleModeEnum>(QuadModeCommandExecute);
            TakeOffCommand = new RelayCommand(TakeOffCommandExecute);
            MissionCommands = new RelayCommand<SoftkeyEnum>(MissionCommandsExecute);
            JoystickRestartCommands = new RelayCommand(JoystickRestartCommandsExecute);
            SetNextWaypointCommand = new RelayCommand(SetNextWaypointCommandExecute);
            SelectTaskCommand = new RelayCommand<object>(SelectTaskCommandExecute);
            SendMissionCommands = new RelayCommand(SendMissionCommandsExecute);
            AltTakeOff = 10;
            Messenger.Default.Register<UpdataMissionMessenger>(this, UpdataMissionMessengerMessenger);
        }

       
        #endregion

        #region  Command Execute

        private void SelectTaskCommandExecute(object obj)
        {
            var vehicle = obj as Vehicle;
            if (vehicle != null)
            {
                vehicle.Tasks.Clear();
                vehicle.Tasks.Add(vehicle.Task);
                vehicle.Task = vehicle.Task;
                vehicle.IsActivateMission = false;
                MissionComponent.UploadTask(vehicle.ID,
                                            vehicle.HomePosition,
                                            new MavlinkWaypoint
                                                {
                                                    Latitude = vehicle.Latitude,
                                                    Longitude = vehicle.Longitude,
                                                    Altitude = DefaultAltitude,
                                                    Param4 = (float)vehicle.Heading
                                                },
                                            vehicle.Task);
                vehicle.MissionID = vehicle.Task.TaskID;
            }
        }

        private void JoystickRestartCommandsExecute()
        {
            foreach (var vehicle in Vehicles.Where(vehicle => vehicle.IsSelected))
            {
                MMAVLink.MAVLink.mavlink_rc_channels_override_t joystick;
                joystick.chan1_raw = 0;
                joystick.chan2_raw = 0;
                joystick.chan3_raw = 0;
                joystick.chan4_raw = 0;
                joystick.chan5_raw = 0;
                joystick.chan6_raw = 0;
                joystick.chan7_raw = 0;
                joystick.chan8_raw = 0;
                joystick.target_system = 1;
                joystick.target_component = 1;

                VehicleComponent.SendData(vehicle.ID, (byte)MMAVLink.MAVLink.MAVLINK_MSG_ID.RC_CHANNELS_OVERRIDE, joystick);
                Thread.Sleep(1000);
                Debug.WriteLine("JoystickRestartCommandsExecute - vehicle " + vehicle.ID);
            }


        }

        private async void SendMissionCommandsExecute()
        {
            await AsyncUtil.RaunAsync(() =>
                {
                    foreach (var vehicle in Vehicles.Where(vehicle => vehicle.IsSelected))
                    {
                        if (vehicle.Task != null)
                        {
                            vehicle.IsActivateMission = false;
                            MissionComponent.UploadTask(vehicle.ID,
                                                        vehicle.HomePosition,
                                                        new MavlinkWaypoint
                                                        {
                                                            Latitude = vehicle.Latitude,
                                                            Longitude = vehicle.Longitude,
                                                            Altitude = DefaultAltitude,
                                                            Param4 = (float)vehicle.Heading
                                                        },
                                                        vehicle.Task); 
                        }
                    }
                    return true;
                });
        }

        #endregion

        #region Messenger
        
        private void UpdataMissionMessengerMessenger(UpdataMissionMessenger obj)
        {
            OnPropertyChanged("Tasks");
        }
        
        #endregion
        
        #region async Command Execute
        
        private async void SetNextWaypointCommandExecute()
        {
            await AsyncUtil.RaunAsync(() =>
            {
                foreach (var vehicle in Vehicles.Where(vehicle => vehicle.IsSelected))
                {
                    var next = vehicle.NextWaypoint + 1;
                    MissionComponent.GoToCurrentWaypointId(vehicle.ID, 0, next);
                    Thread.Sleep(1000);
                }
                return true;
            });
        }

        private async void MissionCommandsExecute(SoftkeyEnum obj)
        {
             await AsyncUtil.RaunAsync(() =>
                 {
                     switch (obj)
                     {
                         case SoftkeyEnum.StartMission:
                             {
                                 foreach (var vehicle in Vehicles.Where(vehicle => vehicle.IsSelected))
                                 {
                                     MissionComponent.StartMission(vehicle.ID, 0);
                                     Thread.Sleep(1000);
                                     Debug.WriteLine("StartMission - vehicle " + vehicle.ID);
                                 }
                             }

                             break;
                         case SoftkeyEnum.StopMission:
                             {
                                 foreach (var vehicle in Vehicles.Where(vehicle => vehicle.IsSelected))
                                 {
                                     MissionComponent.StopMission(vehicle.ID, 0);
                                     Thread.Sleep(1000);
                                     Debug.WriteLine("StopMission - vehicle " + vehicle.ID);
                                 }
                             }

                             break;
                         case SoftkeyEnum.RestartMission:
                             {
                                 foreach (var vehicle in Vehicles.Where(vehicle => vehicle.IsSelected))
                                 {
                                     MissionComponent.RestartMission(vehicle.ID, 0, 0);
                                     Thread.Sleep(1000);
                                     Debug.WriteLine("RestartMission - vehicle " + vehicle.ID);
                                 }
                             }
                             break;
                     }
                     return obj;
                 });

        }

        private async void TakeOffCommandExecute()
        {
            await AsyncUtil.RaunAsync(() =>
                {
                    foreach (var vehicle in Vehicles.Where(vehicle => vehicle.IsSelected))
                    {
                        //if (vehicle.IsActivateMission)
                        {
                            VehicleComponent.TakeOffCommand(vehicle.ID, AltTakeOff, (float) vehicle.Latitude,
                                                            (float) vehicle.Longitude, (float) vehicle.Heading);
                            Thread.Sleep(1000);
                            Debug.WriteLine("TakeOffCommandExecute - vehicle " + vehicle.ID);
                        }
                    }
                    return true;
                });
        }

        private async void ArmModeCommandExecute()
        {
            await AsyncUtil.RaunAsync(() =>
                {
                    foreach (var vehicle in Vehicles.Where(vehicle => vehicle.IsSelected))
                    {
                        VehicleComponent.SetArmDisArmCommand(vehicle.ID, true);
                    }
                    return true;
                });
        }

        private async void DisArmModeCommandExecute()
        {
            await AsyncUtil.RaunAsync(() =>
                {
                    foreach (var vehicle in Vehicles.Where(vehicle => vehicle.IsSelected))
                    {
                        VehicleComponent.SetArmDisArmCommand(vehicle.ID, false);
                    }
                    return true;
                });
        }
        
        private async void QuadModeCommandExecute(VehicleModeEnum obj)
        {
            await AsyncUtil.RaunAsync(() =>
            {
                foreach (var vehicle in Vehicles.Where(vehicle => vehicle.IsSelected))
                {
                    VehicleComponent.SendModeCommand(vehicle.ID, (uint)obj);
                }
                return obj;
            });
        }
        
        #endregion

        #region public Proprty
        
        private int _altTakeOff;
        private bool _selectAll;
        public bool SelectAll
        {
            get { return _selectAll; }
            set
            {
                _selectAll = value;

                foreach (var vehicle in Vehicles)
                {
                    vehicle.IsSelected = SelectAll;
                }
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Task> Tasks
        {
            get
            {
                var mission = MissionSource.Mission as Mission;
                if (mission != null) return mission.Tasks;
                return null;
            }
        }

        public ObservableCollection<IVehicle> Vehicles
        {
            get { return VehiclesSource.Vehicles; }
        }

        public int AltTakeOff
        {
            get { return _altTakeOff; }
            set
            {
                if (value == _altTakeOff) return;
                _altTakeOff = value;
                OnPropertyChanged();
            }
        }
        #endregion

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
