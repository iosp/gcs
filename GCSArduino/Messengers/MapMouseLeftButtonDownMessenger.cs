using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCSArduino.Messengers
{
    public class MapMouseLeftButtonDownMessenger
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int Altitude { get; set; }
    }
}
