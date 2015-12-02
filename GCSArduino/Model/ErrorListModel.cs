using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media;
using Common.Annotations;
using Common.Enums;
using GCSArduino.Enums;

namespace GCSArduino.Model
{
    public class ErrorListModel : INotifyPropertyChanged
    {
        private string _text;
        private SolidColorBrush _errorLevelSolidColorBrush;
        private ErrorLevelEnum _errorLevel;
        private DateTime _dateTime;

        public string Text
        {
            get { return _text; }
            set
            {
                if (value == _text) return;
                _text = value;
                OnPropertyChanged();
            }
        }

        public DateTime DateTime
        {
            get { return _dateTime; }
            set
            {
                if (value.Equals(_dateTime)) return;
                _dateTime = value;
                OnPropertyChanged();
            }
        }

        public SolidColorBrush ErrorLevelSolidColorBrush
        {
            get { return _errorLevelSolidColorBrush; }
            set
            {
                if (Equals(value, _errorLevelSolidColorBrush)) return;
                _errorLevelSolidColorBrush = value;
                OnPropertyChanged();

            }
        }

        public ErrorLevelEnum ErrorLevel
        {
            get { return _errorLevel; }
            set
            {

                _errorLevel = value;
                OnPropertyChanged();
                switch (ErrorLevel)
                {
                    case ErrorLevelEnum.Non:
                        ErrorLevelSolidColorBrush = Brushes.White;
                        break;
                    case ErrorLevelEnum.Low:
                        ErrorLevelSolidColorBrush = Brushes.YellowGreen;
                        break;
                    case ErrorLevelEnum.Middle:
                        ErrorLevelSolidColorBrush = Brushes.Orange;
                        break;
                    case ErrorLevelEnum.Hight:
                        ErrorLevelSolidColorBrush = Brushes.Red;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
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
