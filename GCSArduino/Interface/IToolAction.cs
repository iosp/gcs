using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Common.Enums;
using GalaSoft.MvvmLight.Command;
using Interfaces;
using Task = MissionPackage.Task;

namespace GCSArduino.Interface
{
    public interface IToolAction
    {
        Visibility VisibilityView { get; set; }
        Thickness MarginView { get; set; }
        bool IsJoystickChecked { get; set; }
        ObservableCollection<Task> Tasks { get;}
        RelayCommand<ITask> SelectedTaskCommand { get; set; }
        RelayCommand<object> CommandState { get; set; }
        IVehicle VehicleSelected { get; set; }
        RelayCommand<VehicleModeEnum> CommandsActionMode { get; set; }
    }
}
