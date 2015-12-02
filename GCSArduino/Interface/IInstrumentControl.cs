using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GCSArduino.Model;
using Interfaces;

namespace GCSArduino.Interface
{
    public interface IInstrumentControl
    {
        IVehicle VehicleSelected { get; }
    }
}
