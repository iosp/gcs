
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Common.Annotations;
using Common.Enums;


namespace GCSArduino.Model
{
    public class QuadStatus : INotifyPropertyChanged
    {
        private int _linqQuality;
        private GpsStatusEnum _gpsStatus;
        private int _percentBattery;
       
        private float _batteryVoltage;
        private byte _batteryRemaining;
        private float _batteryCurrent;
       
        private int _groundZSpeed;
        private int _groundYSpeed;
        private int _groundXSpeed;
        private int _altitudeAboveGround;
        private VehicleModeEnum _mode;
        private bool _failsafe;
        private bool _landed;
        private bool _armed;
        private float _airSpeed;
        private float _groundSpeed;
        private float _climb;
        private float _throttle;
        private ErrorQuad _errorQuad;
        private int _satellites;
        private float _hdop;

        public QuadStatus()
        {
            //GpsStatus = GpsStatusEnum.NoFix;
            LinqQuality =0;
            PercentBattery = 0;
            BatteryVoltage = 0;
            ErrorQuad = new ErrorQuad();
        }

        public int TickCount { get; set; }
        public int LinqQuality
        {
            get { return _linqQuality; }
            set
            {
                _linqQuality = value;
                OnPropertyChanged();
            }
        }

        public ErrorQuad ErrorQuad
        {
            get { return _errorQuad; }
            set { _errorQuad = value; OnPropertyChanged();}
        }

        public GpsStatusEnum GpsStatus
        {
            get { return _gpsStatus; }
            set
            {
                _gpsStatus = value;
                OnPropertyChanged();
            }
        }

        public float Hdop
        {
            get { return _hdop; }
            set { _hdop = value; OnPropertyChanged();}
        }

        public int PercentBattery
        {
            get { return _percentBattery; }
            set
            {
                if (value == _percentBattery) return;
                _percentBattery = value;
                OnPropertyChanged();
            }
        }

        public int Satellites 
        {
            get { return _satellites; }
            set { _satellites = value; OnPropertyChanged(); }

        }

        public float BatteryVoltage
        {
            get { return _batteryVoltage; }
            set
            {
                if (value.Equals(_batteryVoltage)) return;
                _batteryVoltage = value;
                OnPropertyChanged();
            }
        }

        public byte BatteryRemaining
        {
            get { return _batteryRemaining; }
            set
            {
                if (value == _batteryRemaining) return;
                _batteryRemaining = value;
                PercentBattery = value;
                OnPropertyChanged();
            }
        }

        public float BatteryCurrent
        {
            get { return _batteryCurrent; }
            set
            {
                if (value.Equals(_batteryCurrent)) return;
                _batteryCurrent = value;
                OnPropertyChanged();
            }
        }

     
        public int AltitudeAboveGround
        {
            get { return _altitudeAboveGround; }
            set
            {
                if (value == _altitudeAboveGround) return;
                _altitudeAboveGround = value;
                OnPropertyChanged();
            }
        }

        public int GroundXSpeed
        {
            get { return _groundXSpeed; }
            set
            {
                if (value == _groundXSpeed) return;
                _groundXSpeed = value;
                OnPropertyChanged();
            }
        }

        public int GroundYSpeed
        {
            get { return _groundYSpeed; }
            set
            {
                if (value == _groundYSpeed) return;
                _groundYSpeed = value;
                OnPropertyChanged();
            }
        }

        public int GroundZSpeed
        {
            get { return _groundZSpeed; }
            set
            {
                if (value == _groundZSpeed) return;
                _groundZSpeed = value;
                OnPropertyChanged();
            }
        }
        public bool Failsafe
        {
            get { return _failsafe; }
            set { _failsafe = value; OnPropertyChanged();}
        }
        public bool Landed
        {
            get { return _landed; }
            set { _landed = value; OnPropertyChanged();}
        }

        public bool Armed
        {
            get { return _armed; }
            set { _armed = value; OnPropertyChanged();}
        }

        public VehicleModeEnum Mode
        {
            get { return _mode; }
            set { _mode = value; OnPropertyChanged();}
        }

        public float AirSpeed
        {
            get { return _airSpeed; }
            set { _airSpeed = value; OnPropertyChanged();}
        }

        public float GroundSpeed
        {
            get { return _groundSpeed; }
            set { _groundSpeed = value; OnPropertyChanged();}
        }

        public float Climb
        {
            get { return _climb; }
            set { _climb = value; OnPropertyChanged();}
        }

        public float Throttle
        {
            get { return _throttle; }
            set { _throttle = value;OnPropertyChanged(); }
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
