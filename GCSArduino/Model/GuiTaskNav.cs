using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ESRI.ArcGIS.Client.Geometry;
using MissionPackage;

namespace GCSArduino.Model
{
    public class GuiTaskNav : GuiTask, INotifyPropertyChanged
    {
        public GuiTaskNav()
        {

        }
        private Shape _shape;
        private MavlinkTaskNav _taskNav;

        public MavlinkTaskNav TaskNav
        {
            get { return _taskNav; }
            set { _taskNav = value; OnPropertyChanged(); }
        }
        public Shape Shape
        {
            get { return _shape; }
            set { _shape = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public void UpdataPoint()
        {
            var polyline = Shape.Geometry as Polyline;
            if (polyline != null)
            {
                foreach (var path in polyline.Paths)
                {

                }
            }
        }
    }
}
