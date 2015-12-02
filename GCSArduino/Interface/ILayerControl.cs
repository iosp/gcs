using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

using GalaSoft.MvvmLight.Command;
using Interfaces;
using MissionPackage;

namespace GCSArduino.Interface
{
    public interface ILayerControl
    {
        ObservableCollection<IVehicle> Vehicles { get; }
        Mission Mission { get; }
        ObservableCollection<Task> Tasks { get; }
        RelayCommand SaveMissionCommand { get; set; }
        RelayCommand OpenMissionCommand { get; set; }
        bool MissionOrVehicles { get; set; }
        Task SelectedTask { get; set; }
        //void DeleteAll();
    }
}
