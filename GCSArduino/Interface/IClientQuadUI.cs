using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Enums;
using GCSArduino.Enums;
using GalaSoft.MvvmLight.Command;
using Interfaces;

namespace GCSArduino.Interface
{
    public interface IClientQuadUI
    {
        IVehicle VehicleSelected { get; set; }
        bool InsertTargetPoint{ get; set; }
        bool InsertHoverPoint { get; set; }
        bool IsCheckedJoystick { get; set; }

        RelayCommand StartMissionCommand { get; set; }
        RelayCommand<VehicleModeEnum> QuadModeCommand { get; set; }
        RelayCommand<SoftkeyEnum> MissionCommands { get; set; }
        RelayCommand JoystickRestartCommands { get; set; }
    }
}
