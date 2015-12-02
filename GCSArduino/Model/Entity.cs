using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Windows.Media;
using Common.Enums;
using ESRI.ArcGIS.Client;
using ESRI.ArcGIS.Client.Geometry;

namespace GCSArduino.Model
{
    public class Entity : Graphic, INotifyPropertyChanged
    {
        private double _xLocation;
        private double _yLocation;
        private double _latitude;
        private double _longitude;
        private float _altitude;

        private Envelope _viewPort;
        private string _text;
        private ImageSource _imageSource;
        private double _heading;
        private bool _visible;
        private string _fileName;
        private long _id;
        private bool _isSelected;
        private bool _armed;
        private int _percentBattery;
        private VehicleModeEnum _vehicleMode;
        private int _missionId;
        private float _timeInWaypoint;
        private bool _missionStatus;
        private string _name;

        public Entity(long id)
        {
            Id = id;
            Text = Id.ToString(CultureInfo.InvariantCulture);
            PercentBattery = 100;
            
        }
        public bool MissionStatus 
        {
            get { return _missionStatus; }
            set { _missionStatus = value; OnPropertyChanged(); } 
        }
        public bool Visible
        {
            get { return _visible; }
            set
            {
                _visible = value;
                OnPropertyChanged();
            }
        }

        public VehicleModeEnum VehicleMode
        {
            get { return _vehicleMode; }
            set { _vehicleMode = value; OnPropertyChanged();}
        }

        public bool Armed
        {
            get { return _armed; }
            set { _armed = value; OnPropertyChanged(); }
        }

        public double Heading
        {
            get { return _heading; }
            set
            {
                _heading = value;
                OnPropertyChanged();
            }
        }
        public float TimeInWaypoint
        {
            get { return _timeInWaypoint; }
            set { _timeInWaypoint = value; OnPropertyChanged();}
        }

        public double XLocation
        {
            get { return _xLocation; }
            set
            {
                _xLocation = value;
                OnPropertyChanged();
            }
        }

        public double YLocation
        {
            get { return _yLocation; }
            set
            {
                _yLocation = value;
                OnPropertyChanged();
            }
        }

        public string Text
        {
            get { return _text; }
            set
            {
                _text = value;
                OnPropertyChanged();
            }
        }

        public int PercentBattery
        {
            get { return _percentBattery; }
            set { _percentBattery = value; OnPropertyChanged();}
        }

        public Envelope ViewPort
        {
            get { return _viewPort; }
            set
            {
                _viewPort = value;
                OnPropertyChanged();
            }
        }

        public ImageSource ImageSource
        {
            get { return _imageSource; }
            set
            {
                _imageSource = value;
                OnPropertyChanged();
            }
        }

        public string FileName
        {
            get { return _fileName; }
            set
            {
                _fileName = value;
                OnPropertyChanged();
            }
        }

        public long Id
        {
            get { return _id; }
            set
            {
                _id = value;
                OnPropertyChanged();
            }
        }

        public bool IsSelected
        {
            get { return _isSelected; }
            set { _isSelected = value;
                OnPropertyChanged();
            }
        }

        public double Longitude
        {
            get { return _longitude; }
            set
            {
                if (value.Equals(_longitude)) return;
                _longitude = value;
                OnPropertyChanged();
            }
        }

        public double Latitude
        {
            get { return _latitude; }
            set
            {
                if (value.Equals(_latitude)) return;
                _latitude = value;
                OnPropertyChanged();
            }
        }

        public float Altitude
        {
            get { return _altitude; }
            set
            {
                if (value.Equals(_altitude)) return;
                _altitude = value;
                OnPropertyChanged();
            }
        }

        public int MissionId
        {
            get { return _missionId; }
            set { _missionId = value; OnPropertyChanged(); }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged();}
        }

        public new event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

    }



}
