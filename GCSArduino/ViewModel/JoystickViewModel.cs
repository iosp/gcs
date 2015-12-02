using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Threading;
using Common.Annotations;
using Common.Utils;
using GCSArduino.Interface;
using GCSArduino.Model;
using Interfaces;
using MMAVLink;
using SlimDX;
using SlimDX.DirectInput;
using System.Threading;
using GalaSoft.MvvmLight.Messaging;
using GCSArduino.Messengers;

namespace GCSArduino.ViewModel
{
    public class JoystickViewModel : IJoystick ,INotifyPropertyChanged
    {
        //int const 
        private const int MaxJoystickRange = 2000;
        private const int MinJoystickRange = 1000;

        private DispatcherTimer SenderTimer;
        private DispatcherTimer UiThread;
        public IVehicleComponent VehicleComponent { get; set; }
        public IMissionComponent MissionComponent { get; set; }

        Joystick joystick { get; set; }
        private JoystickModel _joystickModel;
        JoystickState state = new JoystickState();

        public ObservableCollection<Equaliser> Equalisers
        {
            get { return _equalisers; }
            set
            {
                if (Equals(value, _equalisers)) return;
                _equalisers = value;
                OnPropertyChanged();
            }
        }

        private MAVLink.mavlink_rc_channels_override_t _joystick;
        private ObservableCollection<Equaliser> _equalisers;

        public Vehicle VehicleSelected { get; set; }

        public JoystickViewModel(IVehicleComponent vehicleComponent)
        {
            VehicleComponent = vehicleComponent;
            Messenger.Default.Register<VehicleSelected>(this, VehicleSelectedAction);
            SenderTimer = new DispatcherTimer {Interval = new TimeSpan(0, 0, 0, 0, 1000/12)};
            UiThread = new DispatcherTimer { Interval = new TimeSpan(0, 0, 0, 0, 1000 / 12) };

            UiThread.Tick += UiThread_Tick;
            UiThread.Start();
            _joystick = new MAVLink.mavlink_rc_channels_override_t();
            // make sure that DirectInput has been initialized
            var dinput = new DirectInput();

            // search for devices
            foreach (DeviceInstance device in dinput.GetDevices(DeviceClass.GameController, DeviceEnumerationFlags.AttachedOnly))
            {
                // create the device
                try
                {
                    joystick = new Joystick(dinput, device.InstanceGuid);
                   
                    break;
                }
                catch (DirectInputException exception)
                {
                    Logger.Error("JoystickViewModel", exception);
                }
            }

            if (joystick == null)
            {
                //MessageBox.Show("There are no joysticks attached to the system.");
                return;
            }
            foreach (DeviceObjectInstance deviceObject in joystick.GetObjects())
            {
                if ((deviceObject.ObjectType & ObjectDeviceType.Axis) != 0)
                    joystick.GetObjectPropertiesById((int)deviceObject.ObjectType).SetRange(MinJoystickRange, MaxJoystickRange);

            }
            Equalisers = new ObservableCollection<Equaliser>();
            for (int i = 0; i < 12; i++)
            {
                Equalisers.Add(new Equaliser { Id = i+1, Channel = 1, Value = 0, IsChecked = false, Receiver = 0 });    
            }
        }

        void UiThread_Tick(object sender, EventArgs e)
        {
            if (VehicleSelected != null && Equalisers != null)
            {
                Equalisers[0].Value = 256*(-1000 + (ushort) VehicleSelected.JoystickIn.Roll)/1000;
                Equalisers[1].Value = 256*(-1000 + (ushort) VehicleSelected.JoystickIn.Pitch)/1000;
                Equalisers[2].Value = 256*(-1000 + (ushort) VehicleSelected.JoystickIn.Throttle)/1000;
                Equalisers[3].Value = 256*(-1000 + (ushort) VehicleSelected.JoystickIn.Rudder)/1000;
            }
        }

        private void VehicleSelectedAction(VehicleSelected obj)
        {
            if (obj.Vehicle == null) return;
            VehicleId = obj.Vehicle.ID;
            VehicleSelected = (Vehicle) obj.Vehicle;
        }
        private void SenderTimerTick(object sender, EventArgs e)
        {
            try
            {
                if (VehicleId == 0) return;

                if (joystick == null )return;
                if (joystick.Acquire().IsFailure)
                    return;

                if (joystick.Poll().IsFailure)
                    return;

                state = joystick.GetCurrentState();
                if (Result.Last.IsFailure)
                    return;

                var buttons = state.GetButtons();
                
                //Equalisers[0].Value = 256 * (-1000 + (ushort)state.Z) / 1000;
                //Equalisers[1].Value = 256 * (-1000 + (ushort)state.RotationZ) / 1000;
                //Equalisers[2].Value = 256 * (-1000 + (ushort)state.Y) / 1000;
                //Equalisers[3].Value = 256 * (-1000 + (ushort)state.X) / 1000;

                //Equalisers[4].Value = buttons[0] ? 256 : 0;
                //Equalisers[5].Value = buttons[1] ? 256 : 0;
                //Equalisers[6].Value = buttons[2] ? 256 : 0;
                //Equalisers[7].Value = buttons[3] ? 256 : 0;

                //Equalisers[8].Value = buttons[4] ? 256 : 0;
                //Equalisers[9].Value = buttons[5] ? 256 : 0;
                //Equalisers[10].Value = buttons[6] ? 256 : 0;
                //Equalisers[11].Value = buttons[7] ? 256 : 0;

                
                _joystick.chan1_raw = (ushort)state.Z;
                _joystick.chan2_raw = (ushort)state.RotationZ;
                _joystick.chan3_raw = (ushort) ((MaxJoystickRange - state.Y) + 1000);
                _joystick.chan4_raw = (ushort)state.X;
                _joystick.chan5_raw = (ushort)(buttons[4] ? 1000 : -1000);
                _joystick.chan6_raw = (ushort)(buttons[5] ? 1000 : -1000);
                _joystick.chan7_raw = (ushort)(buttons[6] ? 1000 : -1000);
                _joystick.chan8_raw = (ushort)(buttons[7] ? 1000 : -1000);
                _joystick.target_system = 1;
                _joystick.target_component = 1;

                if (buttons[4])
                    VehicleComponent.SendCommand(VehicleId, (ushort)MAVLink.MAV_CMD.COMPONENT_ARM_DISARM, new List<float> { 1, 0, 0, 0, 0, 0, 0 });
                if (buttons[5])
                    VehicleComponent.SendCommand(VehicleId, (ushort)MAVLink.MAV_CMD.COMPONENT_ARM_DISARM, new List<float> { 1, 0, 0, 0, 0, 0, 0 });

                VehicleComponent.SendData(VehicleId, (byte)MAVLink.MAVLINK_MSG_ID.RC_CHANNELS_OVERRIDE, _joystick);
            }
            catch (Exception exception)
            {
                Logger.Error("SenderTimerTick", exception);
               
            }
            
        }

        public JoystickModel joystickModel
        {
            get { return _joystickModel; }
            set
            {
                if (Equals(value, _joystickModel)) return;
                _joystickModel = value;
                OnPropertyChanged();
            }
        }

        public long VehicleId { get; set; }
        
        public void Start(long idx)
        {
            VehicleId = idx;
            SenderTimer.Tick += SenderTimerTick;
            SenderTimer.Start();
        }

        public void Stop()
        {
          
            SenderTimer.Stop();
            SenderTimer.Tick -= SenderTimerTick;


            _joystick.chan1_raw = 0;
            _joystick.chan2_raw = 0;
            _joystick.chan3_raw = 0;
            _joystick.chan4_raw = 0;
          
            _joystick.target_system = 1;
            _joystick.target_component = 1;

            for (int i = 0; i < 2; i++)
            {
                Thread.Sleep(100);
                VehicleComponent.SendData(VehicleId, (byte)MAVLink.MAVLINK_MSG_ID.RC_CHANNELS_OVERRIDE, _joystick);    
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
