using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Class;

namespace GCSArduino.Messengers
{
    public class ReadMissionMessenger
    {
        public int Vehicle { get; set; }
        public ObservableCollection<Waypoint> Waypoints { get; set; }
    }
}
