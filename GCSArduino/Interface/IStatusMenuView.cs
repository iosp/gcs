using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GCSArduino.Enums;
using GCSArduino.Model;
using Interfaces;

namespace GCSArduino.Interface
{
    public interface IStatusMenuView
    {
     
        DateTime DateTime { get; set; }
        IVehicle VehicleSelected { get; }
    }
}
