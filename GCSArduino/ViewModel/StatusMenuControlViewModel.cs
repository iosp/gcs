using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using Common.Annotations;
using GCSArduino.Enums;
using GCSArduino.Interface;
using GCSArduino.Messengers;
using GCSArduino.Model;
using GalaSoft.MvvmLight.Messaging;
using Interfaces;
using Microsoft.Practices.ServiceLocation;

namespace GCSArduino.ViewModel
{
    internal class StatusMenuControlViewModel : IStatusMenuView, INotifyPropertyChanged
    {
        private readonly DispatcherTimer _dispatcherUiTimer;
        private DateTime _dateTime;
        private const int UpdataTimer = 100;

        private IVehiclesSource VehiclesSource { get; set; }
              
        public StatusMenuControlViewModel(IVehiclesSource vehiclesSource)
        {
            VehiclesSource = vehiclesSource;

            _dispatcherUiTimer = new DispatcherTimer {Interval = new TimeSpan(0, 0, 0, 0, UpdataTimer)};
            _dispatcherUiTimer.Tick += DispatcherUiTimerTick;
            _dispatcherUiTimer.Start();
            //VehicleSelected = vehiclesSource.VehicleSelected;
            Messenger.Default.Register<VehicleSelected>(this, VehicleSelectedAction);
           
        }
        private IVehicle _vehicleSelected;
        public IVehicle VehicleSelected
        {
            get { return _vehicleSelected; }
            set { _vehicleSelected = value; OnPropertyChanged(); }
        }

        private void VehicleSelectedAction(VehicleSelected obj)
        {
            if (obj.Vehicle == null) return;
            VehicleSelected = obj.Vehicle;
        }

        private void DispatcherUiTimerTick(object sender, EventArgs e)
        {
            UpdateStatusBar();
            CheckLinkQualityTask();
        }

        private void UpdateStatusBar()
        {
            DateTime = DateTime.Now;
        }
        private void CheckLinkQualityTask()
        {

            var vehicle = VehicleSelected as GuiQuadVehicle;

            if (vehicle != null)
            {
                if (Environment.TickCount - vehicle.QuadStatus.TickCount < 1000)
                    vehicle.QuadStatus.LinqQuality = 100;
                else if (Environment.TickCount - vehicle.QuadStatus.TickCount < 1500)
                    vehicle.QuadStatus.LinqQuality = 80;
                else if (Environment.TickCount - vehicle.QuadStatus.TickCount < 200)
                    vehicle.QuadStatus.LinqQuality = 60;
                else if (Environment.TickCount - vehicle.QuadStatus.TickCount < 3000)
                    vehicle.QuadStatus.LinqQuality = 40;
                else if (Environment.TickCount - vehicle.QuadStatus.TickCount < 5000)
                    vehicle.QuadStatus.LinqQuality = 20;
                else if (Environment.TickCount - vehicle.QuadStatus.TickCount > 10000)
                    vehicle.QuadStatus.LinqQuality = 0;

                //Debug.WriteLine("CheckLinkQualityTask - " + (Environment.TickCount - vehicle.QuadStatus.TickCount));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
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


        ~StatusMenuControlViewModel()
        {
            _dispatcherUiTimer.Tick -= DispatcherUiTimerTick;
        }
    }
}
