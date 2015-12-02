using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Annotations;
using GCSArduino.Interface;
using Interfaces;

namespace GCSArduino.ViewModel
{
    public class StatusViewModel : INotifyPropertyChanged,IStatus
    {
        #region Property
        
        private double _text00;
        private double _text01;
        private double _text02;
        private double _text03;
        private double _text04;
        private double _text05;
        private double _text06;
        private double _text10;
        private double _text11;
        private double _text12;
        private double _text13;
        private double _text14;
        private double _text15;
        private double _text16;

        public double Text00
        {
            get { return _text00; }
            set { _text00 = value; OnPropertyChanged("Text16"); }
        }

        public double Text01
        {
            get { return _text01; }
            set { _text01 = value; OnPropertyChanged("Text16"); }
        }

        public double Text02
        {
            get { return _text02; }
            set { _text02 = value; OnPropertyChanged("Text16"); }
        }

        public double Text03
        {
            get { return _text03; }
            set { _text03 = value; OnPropertyChanged("Text16"); }
        }

        public double Text04
        {
            get { return _text04; }
            set { _text04 = value; OnPropertyChanged("Text16"); }
        }

        public double Text05
        {
            get { return _text05; }
            set { _text05 = value; OnPropertyChanged("Text16"); }
        }

        public double Text06
        {
            get { return _text06; }
            set { _text06 = value; OnPropertyChanged("Text16"); }
        }

        public double Text10
        {
            get { return _text10; }
            set { _text10 = value; OnPropertyChanged("Text16"); }
        }

        public double Text11
        {
            get { return _text11; }
            set { _text11 = value; OnPropertyChanged("Text16"); }
        }

        public double Text12
        {
            get { return _text12; }
            set { _text12 = value; OnPropertyChanged("Text16"); }
        }

        public double Text13
        {
            get { return _text13; }
            set { _text13 = value; OnPropertyChanged("Text16"); }
        }

        public double Text14
        {
            get { return _text14; }
            set { _text14 = value; OnPropertyChanged("Text16"); }
        }

        public double Text15
        {
            get { return _text15; }
            set { _text15 = value; OnPropertyChanged("Text16"); }
        }

        public double Text16
        {
            get { return _text16; }
            set { _text16 = value; OnPropertyChanged("Text16"); }
        }
        #endregion
       
        public StatusViewModel()
        {
            Text00 = 10;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
