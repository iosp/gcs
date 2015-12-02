using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Class;
using ESRI.ArcGIS.Client;
using GalaSoft.MvvmLight.Command;
using Interfaces;
using MissionPackage;
using Task = MissionPackage.Task;

namespace GCSArduino.Interface
{
    public interface IListWaypoint
    {
        //IVehicle VehicleSelected { get; set; }
        Waypoint SelectedWaypoint { get; set; }
        Task TaskNav { get; set; }
    }
}
