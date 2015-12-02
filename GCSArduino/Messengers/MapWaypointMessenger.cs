using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GCSArduino.Messengers
{
    public class MapWaypointMessenger
    {
        public MapWaypointMessenger()
        {
            Points = new List<Point>();
        }
        public List<Point> Points { get; set; }
        public int TaskId { get; set; }
    }
}
