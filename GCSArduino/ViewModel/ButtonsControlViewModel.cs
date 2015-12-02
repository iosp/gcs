using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows;
using Common.Class;
using Common.Enums;
using Common.Utils;
using GCSArduino.Enums;
using GCSArduino.Interface;
using GCSArduino.Messengers;
using GCSArduino.Model;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Interfaces;
using MMAVLink;
using Microsoft.Practices.ServiceLocation;

namespace GCSArduino.ViewModel
{
    public class ButtonsControlViewModel : BaseViewModel //, IButtonsControl
    {
        private bool _armDisArmIsChecked;
        private bool _loiterIsChecked;
        private Visibility _visibility;
        private IVehicle _vehicleSelected;
        public RelayCommand<SoftkeyEnum> ButtonCommands { get; set; }
        public RelayCommand<VehicleModeEnum> QuadModeCommand { get; set; } 
        public IVehicleComponent VehicleComponent { get; set; }
        public IMissionComponent MissionComponent { get; set; }

        public IVehicle VehicleSelected
        {
            get { return _vehicleSelected; }
            set { _vehicleSelected = value; OnPropertyChanged();}
        }

        public ButtonsControlViewModel(IVehicleComponent vehicleComponent, IMissionComponent missionComponent)
        {
            VehicleComponent = vehicleComponent;
            MissionComponent = missionComponent;
            ButtonCommands = new RelayCommand<SoftkeyEnum>(ButtonCommandsAction);
            QuadModeCommand = new RelayCommand<VehicleModeEnum>(QuadModeCommandAction);
            Messenger.Default.Register<VehicleSelected>(this, VehicleSelectedAction);
           
                
        }

        private void QuadModeCommandAction(VehicleModeEnum obj)
        {
            if (VehicleSelected != null)
                VehicleComponent.SendModeCommand(VehicleSelected.ID, (uint)obj);
        }

        private void VehicleSelectedAction(VehicleSelected obj)
        {
            if (obj.Vehicle == null) return;
            VehicleSelected = obj.Vehicle;
        }

        public Visibility Visibility
        {
            get { return _visibility; }
            set { _visibility = value;OnPropertyChanged(); }
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
                case SoftkeyEnum.Reserve:
                    VehicleComponent.TakeOffCommand(VehicleSelected.ID, 100, (float) VehicleSelected.Latitude,
                                                    (float) VehicleSelected.Longitude, (float) VehicleSelected.Heading);

                   
                    break;
                default:
                    throw new ArgumentOutOfRangeException("obj");
            }
        }

        private void RestartMission()
        {
            if (VehicleSelected != null)
                MissionComponent.RestartMission(VehicleSelected.ID, 0,0);
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

        public void GotoWaypoint(float Latitude,float longitude,float altitude,float param1,float param2,float param3 , float  angle)
        {
            if (VehicleSelected != null)
            {
                var gotoWaypoint = new MavlinkWaypoint
                    {
                        Latitude = Latitude,
                        Longitude = longitude,
                        Altitude = altitude,
                        Id = 0,
                        Command = (ushort) MAVLink.MAV_CMD.OVERRIDE_GOTO,
                        Param1 = param1,
                        Param2 = param2,
                        Param3 = param3,
                        Param4 = angle,
                    };

                VehicleComponent.GotoWaypointCommand(VehicleSelected.ID, gotoWaypoint);
            }
        }

        #region Property

        public bool ArmDisArmIsChecked
        {
            get { return _armDisArmIsChecked; }
            set
            {
                if (value.Equals(_armDisArmIsChecked)) return;
                _armDisArmIsChecked = value;
                OnPropertyChanged();
            }
        }

        public bool LoiterIsChecked
        {
            get { return _loiterIsChecked; }
            set
            {
                if (value.Equals(_loiterIsChecked)) return;
                _loiterIsChecked = value;
                OnPropertyChanged();
            }
        }

        #endregion
    }
}
