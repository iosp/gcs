using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using GCSArduino.Annotations;

namespace GCSArduino.Model
{
    public class ErrorQuad : INotifyPropertyChanged
    {
        private bool _gps;
        private bool _gyro;
        private bool _accel;
        private bool _compass;
        private bool _baro;
        private bool _optFlow;
        private bool _rc;
        private bool _battery;
        private bool _hdop;
        private bool _satellites;

        public ErrorQuad()
        {
         
        }
        public bool Gps
        {
            get { return _gps; }
            set
            {
                if (value.Equals(_gps)) return;
                _gps = value;
                OnPropertyChanged();
            }
        }

        public bool Gyro
        {
            get { return _gyro; }
            set
            {
                if (value.Equals(_gyro)) return;
                _gyro = value;
                OnPropertyChanged();
            }
        }

        public bool Accel
        {
            get { return _accel; }
            set
            {
                if (value.Equals(_accel)) return;
                _accel = value;
                OnPropertyChanged();
            }
        }

        public bool Compass
        {
            get { return _compass; }
            set
            {
                if (value.Equals(_compass)) return;
                _compass = value;
                OnPropertyChanged();
            }
        }

        public bool Baro
        {
            get { return _baro; }
            set
            {
                if (value.Equals(_baro)) return;
                _baro = value;
                OnPropertyChanged();
            }
        }

        public bool OptFlow
        {
            get { return _optFlow; }
            set
            {
                if (value.Equals(_optFlow)) return;
                _optFlow = value;
                OnPropertyChanged();
            }
        }

        public bool Rc
        {
            get { return _rc; }
            set
            {
                if (value.Equals(_rc)) return;
                _rc = value;
                OnPropertyChanged();
            }
        }

        public bool Battery
        {
            get { return _battery; }
            set
            {
                if (value.Equals(_battery)) return;
                _battery = value;
                OnPropertyChanged();
            }
        }

        public bool Hdop
        {
            get { return _hdop; }
            set
            {
                if (value.Equals(_hdop)) return;
                _hdop = value;
                OnPropertyChanged();
            }
        }

        public bool Satellites
        {
            get { return _satellites; }
            set
            {
                if (value.Equals(_satellites)) return;
                _satellites = value;
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
