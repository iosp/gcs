using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using Common.AsyncFunction;
using Common.Class;
using Common.Enums;
using Common.Utils;
using ESRI.ArcGIS.Client;
using ESRI.ArcGIS.Client.Symbols;
using GCSArduino.Annotations;
using GCSArduino.Enums;
using GCSArduino.Interface;
using GCSArduino.Messengers;
using GCSArduino.Model;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Interfaces;
using Microsoft.Practices.ServiceLocation;
using MissionPackage;

namespace GCSArduino.ViewModel
{
    public class ClientQuadUIViewModel : IClientQuadUI, INotifyPropertyChanged
    {
        public IMissionComponent MissionComponent { get; set; }
        public IVehicleComponent VehicleComponent { get; set; }
        public IMissionSource MissionSource { get; set; }
        public RelayCommand StartMissionCommand { get; set; }
        public RelayCommand<VehicleModeEnum> QuadModeCommand { get; set; }
        public RelayCommand<SoftkeyEnum> MissionCommands { get; set; }
        public RelayCommand JoystickRestartCommands { get; set; }

        private const int TaskID = 10001;
        const int DefaultAltitude = 100;

        public ClientQuadUIViewModel(IToolAction toolAction, IMissionComponent missionComponent, IVehicleComponent vehicleComponent, IMissionSource missionSource)
        {
            VehicleSelected = toolAction.VehicleSelected;
            MissionComponent = missionComponent;
            VehicleComponent = vehicleComponent;
            MissionSource = missionSource;

            StartMissionCommand = new RelayCommand(StartMissionExecute);
            QuadModeCommand = new RelayCommand<VehicleModeEnum>(ArduModeCommandExecute);
            MissionCommands = new RelayCommand<SoftkeyEnum>(MissionCommandsExecute);
            JoystickRestartCommands = new RelayCommand(JoystickRestartCommandsExecute);
            
            Messenger.Default.Register<VehicleSelected>(this, VehicleSelectedAction);

            Messenger.Default.Register<MapMouseLeftButtonDownMessenger>(this, MapMouseLeftButtonDownAction);

            Logger.Info("ClientQuadUIViewModel init");
        }

        #region Commands Function
        private void JoystickRestartCommandsExecute()
        {
            MMAVLink.MAVLink.mavlink_rc_channels_override_t _joystick;
            _joystick.chan1_raw = 0;
            _joystick.chan2_raw = 0;
            _joystick.chan3_raw = 0;
            _joystick.chan4_raw = 0;
            _joystick.chan5_raw = 0;
            _joystick.chan6_raw = 0;
            _joystick.chan7_raw = 0;
            _joystick.chan8_raw = 0;
            _joystick.target_system = 1;
            _joystick.target_component = 1;

            VehicleComponent.SendData(VehicleSelected.ID, (byte)MMAVLink.MAVLink.MAVLINK_MSG_ID.RC_CHANNELS_OVERRIDE, _joystick);
        }

        private void MissionCommandsExecute(SoftkeyEnum obj)
        {
            if (VehicleSelected == null) return;
            switch (obj)
            {
                case SoftkeyEnum.ArmDisArm:
                    var guiQuadVehicle = VehicleSelected as GuiQuadVehicle;
                    VehicleComponent.SetArmDisArmCommand(VehicleSelected.ID, !guiQuadVehicle.QuadStatus.Armed);
                    break;
                case SoftkeyEnum.Loiter:
                    break;
                case SoftkeyEnum.ReturnToLaunch:
                    break;
                case SoftkeyEnum.SetHome:
                    break;
                case SoftkeyEnum.StartMission:
                    MissionComponent.StartMission(VehicleSelected.ID, 0);
                    break;
                case SoftkeyEnum.StopMission:
                    MissionComponent.StopMission(VehicleSelected.ID, 0);
                    break;
                case SoftkeyEnum.RestartMission:
                    MissionComponent.RestartMission(VehicleSelected.ID, 0, 0);
                    break;
                case SoftkeyEnum.Auto:
                    break;
                case SoftkeyEnum.Sensor:
                    break;
                case SoftkeyEnum.Reserve:
                    break;
                case SoftkeyEnum.TaskOff:
                    VehicleComponent.TakeOffCommand(VehicleSelected.ID, 100, (float)VehicleSelected.Latitude,
                                                    (float)VehicleSelected.Longitude, (float)VehicleSelected.Heading);
                    break;
                default:
                    throw new ArgumentOutOfRangeException("obj");
            }
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

        private async void StartMissionExecute()
        {

            if (VehicleSelected != null)
            {
                var mission = await AsyncUtil.RaunAsync<bool>(StartMissionAsync);

            }
        }

        private bool StartMissionAsync()
        {
            if (VehicleSelected == null) return false;

            MissionComponent.StartMissionAuto(VehicleSelected.ID, 0);
           
            return true;
        }
        #endregion
        

        private void MapMouseLeftButtonDownAction(MapMouseLeftButtonDownMessenger obj)
        {
            int DistanceDefault =50;
            if (VehicleSelected == null) return;

            if (InsertHoverPoint)
            {
                //Target
                //var a = Utils.Utils.DegreeBearing(VehicleSelected.Latitude, VehicleSelected.Longitude, obj.Latitude,obj.Longitude);

                var convertPoint = new ObservableCollection<Waypoint>();

                var x = VehicleSelected.Latitude;
                var y = VehicleSelected.Longitude;
                //Utils.Utils.ConvertMercatorToGeo(ref y, ref x);
                /*var bearing = Utils.Utils.DegreeBearing(VehicleSelected.Latitude, VehicleSelected.Longitude, obj.Latitude,obj.Longitude);
                var distance = Utils.Utils.CalculateDistance(VehicleSelected.Latitude, VehicleSelected.Longitude,
                                                             obj.Latitude, obj.Longitude, "K") *1000;
                if (distance > DistanceDefault)
                {
                    Utils.Utils.PointRadialDistance(ref x, ref y, bearing, DistanceDefault);
                }*/
                convertPoint.Add(new MavlinkWaypoint
                    {
                        Latitude = x,
                        Longitude = y,
                        Altitude = DefaultAltitude,
                        Id = 1,
                        Param4 = (float) VehicleSelected.Heading,
                        Command = (ushort) MissionMavCmd.WAYPOINT,

                    });

                convertPoint.Add(new MavlinkWaypoint
                    {
                        Latitude = obj.Latitude,
                        Longitude = obj.Longitude,
                        Altitude = DefaultAltitude,
                        Id = 2,
                        Command = (ushort) MissionMavCmd.WAYPOINT,

                    });


                var task = new MavlinkTaskNav("HoverTask", TaskID)
                    {
                        NavPath = convertPoint,
                        TaskType = TaskTypeEnum.Hoverrer
                    };

                MissionSource.AddTask(task);
                VehicleSelected.Tasks.Clear();
                VehicleSelected.Tasks.Add(task);
                var newShape = new Shape(TaskID, convertPoint)
                    {
                        Symbol = (LineSymbol) Application.Current.FindResource("LineSymbol")
                    };

                ServiceLocator.Current.GetInstance<IMap>().AddShape(newShape);
                
                //MissionComponent.UploadTask(VehicleSelected.ID,
                //                            VehicleSelected.HomePosition,
                //                            new MavlinkWaypoint
                //                                {
                //                                    Latitude = VehicleSelected.Latitude,
                //                                    Longitude = VehicleSelected.Longitude,
                //                                    Altitude = DefaultAltitude,
                //                                    Param4 = (float) VehicleSelected.Heading
                //                                },
                //                            VehicleSelected.Tasks[0]);
                VehicleSelected.IsActivateMission = false;
                MissionComponent.UploadTask(VehicleSelected.ID,VehicleSelected.HomePosition,null,VehicleSelected.Tasks[0]);

                VehicleSelected.MissionID = VehicleSelected.Tasks[0].TaskID;
            }

        }

        private void VehicleSelectedAction(VehicleSelected obj)
        {
            if(obj.Vehicle == null) return;
            VehicleSelected = obj.Vehicle;
        }

        #region Property
        private bool _insertTargetPoint;
        private bool _insertHoverPoint;
        private IVehicle _vehicleSelected;
        private bool _isCheckedJoystick;


        public IVehicle VehicleSelected
        {
            get { return _vehicleSelected; }
            set { _vehicleSelected = value; OnPropertyChanged(); }
        }
        public bool InsertTargetPoint
        {
            get { return _insertTargetPoint; }
            set
            {
                if (value.Equals(_insertTargetPoint)) return;
                _insertTargetPoint = value;
                if(value)
                    ServiceLocator.Current.GetInstance<IMap>().DrawMode = DrawMode.Point;
                OnPropertyChanged();
            }
        }

        public bool InsertHoverPoint
        {
            get { return _insertHoverPoint; }
            set
            {
                if (value.Equals(_insertHoverPoint)) return;
                _insertHoverPoint = value;
                if (value)
                OnPropertyChanged();
            }
        }

        public bool IsCheckedJoystick
        {
            get { return _isCheckedJoystick; }
            set
            {
                if (value.Equals(_isCheckedJoystick)) return;
                _isCheckedJoystick = value;

                if (value)
                {
                    if (VehicleSelected != null)
                    {
                        //VehicleComponent.SendModeCommand(VehicleSelected.ID, (uint) VehicleModeEnum.Loiter);
                        ServiceLocator.Current.GetInstance<IJoystick>().Start(VehicleSelected.ID);
                    }
                }
                else
                {
                    if (VehicleSelected != null)
                    {
                        //VehicleComponent.SendModeCommand(VehicleSelected.ID, (uint) VehicleModeEnum.Auto);
                        ServiceLocator.Current.GetInstance<IJoystick>().Stop();
                    }
                }
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
