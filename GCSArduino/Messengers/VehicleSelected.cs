using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Common.Enums;
using Interfaces;

namespace GCSArduino.Messengers
{
    public class VehicleSelected
    {
        public IVehicle Vehicle { get; set; }
        public MouseClickEnum  MouseClick{ get; set; }
        public Thickness Thickness { get; set; }
    }
}
