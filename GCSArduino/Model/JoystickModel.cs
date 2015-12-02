using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace GCSArduino.Model
{
    public class JoystickModel : INotifyPropertyChanged
    {
      

        public JoystickModel()
        {
            
        }
        private int _roll;
        public int Roll
        {
            get { return _roll; }
            set { _roll = value; OnPropertyChanged(); }
        }
        private int _pitch;
        public int Pitch
        {
            get { return _pitch; }
            set
            {
                _pitch = value;
                OnPropertyChanged();
            }
        }

        private int _throttle;
        public int Throttle
        {
            get { return _throttle; }
            set
            {
                _throttle = value;
                OnPropertyChanged();
            }
        }

        private int _rudder;
        private bool _button1;
        private bool _button2;
        private bool _button3;
        private bool _button4;
        private bool _button5;
        private bool _button6;
        private bool _button7;
        private bool _button8;

        public int Rudder
        {
            get { return _rudder; }
            set
            {
                _rudder = value;
                OnPropertyChanged();
            }
        }
        public bool Button1
        {
            get { return _button1; }
            set { _button1 = value; OnPropertyChanged();}
        }

        public bool Button2
        {
            get { return _button2; }
            set { _button2 = value; OnPropertyChanged();}
        }

        public bool Button3
        {
            get { return _button3; }
            set { _button3 = value; OnPropertyChanged();}
        }

        public bool Button4
        {
            get { return _button4; }
            set { _button4 = value; OnPropertyChanged();}
        }

        public bool Button5
        {
            get { return _button5; }
            set { _button5 = value; OnPropertyChanged();}
        }

        public bool Button6
        {
            get { return _button6; }
            set { _button6 = value; OnPropertyChanged();}
        }

        public bool Button7
        {
            get { return _button7; }
            set { _button7 = value; OnPropertyChanged();}
        }

        public bool Button8
        {
            get { return _button8; }
            set { _button8 = value;OnPropertyChanged(); }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        [Common.Annotations.NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
