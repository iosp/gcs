using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Interfaces;

namespace GCSArduino.Interface
{
    public interface IQuadProperty
    {
        IVehicle VehicleSelected { get; set; }
        Visibility ButtonsControlVisibility { get; set; }
        Visibility ModeControlVisibility { get; set; }
    }
}
