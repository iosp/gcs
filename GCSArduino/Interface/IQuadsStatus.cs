using System.Collections.ObjectModel;
using Common.Enums;
using GCSArduino.Enums;
using GalaSoft.MvvmLight.Command;
using Interfaces;
using MissionPackage;

namespace GCSArduino.Interface
{
    public interface IQuadsStatus
    {
        ObservableCollection<IVehicle> Vehicles { get; }
        RelayCommand ArmModeCommand { get; set; }
        RelayCommand DisArmModeCommand { get; set; }
        RelayCommand TakeOffCommand { get; set; }
        RelayCommand JoystickRestartCommands { get; set; }
        RelayCommand SetNextWaypointCommand { get; set; }
        RelayCommand<VehicleModeEnum> QuadModeCommand { get; set; }
        ObservableCollection<Task> Tasks { get; }
        RelayCommand<SoftkeyEnum> MissionCommands { get; set; }
        RelayCommand<object> SelectTaskCommand { get; set; }
        RelayCommand SendMissionCommands { get; set; }
        bool SelectAll { get; set; }
        int AltTakeOff { get; set; } 
    }
}
