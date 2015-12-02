using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using Common.Annotations;
using Common.Class;
using ESRI.ArcGIS.Client.Geometry;
using ESRI.ArcGIS.Client.Symbols;

namespace GCSArduino.Model
{
    public class Shape : INotifyPropertyChanged 
    {
        private int _id;
        private Symbol _symbol;
        private Geometry _geometry;

        public Shape()
        {
            
        }
        public Shape(int id)
        {
            ID = id;
        }

        public Shape(int id, IEnumerable<Waypoint> points)
        {
            var polyline = new Polyline();
            var geometryPointCollection = new PointCollection();
            foreach (var point in points)
            {
                geometryPointCollection.Add(new MapPoint { X = point.Longitude, Y = point.Latitude });
            }
            polyline.Paths.Add(geometryPointCollection);
            Geometry = polyline;
            ID = id;
        }

        public int ID
        {
            get { return _id; }
            set
            {
                if (value == _id) return;
                _id = value;
                OnPropertyChanged();
            }
        }
        public Symbol Symbol
        {
            get { return _symbol; }
            set
            {
                if (Equals(value, _symbol)) return;
                _symbol = value;
                OnPropertyChanged();
            }
        }
        public Geometry Geometry
        {
            get { return _geometry; }
            set
            {
                if (Equals(value, _geometry)) return;
                _geometry = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
