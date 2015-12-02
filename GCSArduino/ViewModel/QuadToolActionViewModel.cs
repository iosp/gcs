using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using Common.Annotations;
using Common.Class;
using Common.Enums;
using GCSArduino.Interface;
using GCSArduino.Messengers;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Interfaces;
using Microsoft.Practices.ServiceLocation;
using MissionPackage;

namespace GCSArduino.ViewModel
{
    public class QuadToolActionViewModel :INotifyPropertyChanged, IToolAction
    {
        private Visibility _visibilityView;
        private Thickness _marginView;
        private IVehicle _vehicleSelected;
        
        public RelayCommand<object> CommandState { get; set; }
        public RelayCommand<ITask> SelectedTaskCommand { get; set; }
        public RelayCommand<VehicleModeEnum> CommandsActionMode { get; set; }

        public IVehicleComponent VehicleComponent { get; set; }
        public IMissionComponent MissionComponent { get; set; }
        const int DefaultAltitude = 100;

        public IVehicle VehicleSelected
        {
            get { return _vehicleSelected; }
            set
            {
                if (value == _vehicleSelected) return;
                _vehicleSelected = value;
                //ServiceLocator.Current.GetInstance<IJoystick>().Stop();
                OnPropertyChanged();
            }
        }

        
       

        public ObservableCollection<Task> Tasks
        {
            get
            {
                var mission = MissionSource.Mission as Mission;
                    if (mission != null) return mission.Tasks;
                return null;
            }
        }

        public IVehiclesSource VehiclesSource { get; set; }
        public IMissionSource MissionSource { get; set; }

        public QuadToolActionViewModel(IVehiclesSource vehiclesSource, IMissionSource missionSource, IVehicleComponent vehicleComponent , IMissionComponent missionComponent)
        {
            VehicleComponent = vehicleComponent;
            VehiclesSource = vehiclesSource;
            MissionSource = missionSource;
            MissionComponent = missionComponent;
            CommandState = new RelayCommand<object>(CommandStateClick);
            SelectedTaskCommand = new RelayCommand<ITask>(SelectedTaskCommandExecute);
            Messenger.Default.Register<VehicleSelected>(this, VehicleSelectedFunction);
            VisibilityView = Visibility.Hidden;
            CommandsActionMode = new RelayCommand<VehicleModeEnum>(CommandsActionModeExecute);

            Messenger.Default.Register<UpdataMissionMessenger>(this,UpdataMissionMessengerMessenger);
        }

        private void UpdataMissionMessengerMessenger(UpdataMissionMessenger obj)
        {
          OnPropertyChanged("Tasks");
        }

        private void CommandsActionModeExecute(VehicleModeEnum obj)
        {
            VehicleComponent.SendModeCommand(VehicleSelected.ID, (uint)obj);
        }

        private void SelectedTaskCommandExecute(ITask obj)
        {
            foreach (var vehicle in VehiclesSource.Vehicles)
            {
                if (vehicle.ID == VehicleSelected.ID)
                {
                    vehicle.Tasks.Clear();
                    vehicle.Tasks.Add(obj);
                    vehicle.Task = obj;
                    vehicle.IsActivateMission = false;
                    MissionComponent.UploadTask(vehicle.ID,
                                                vehicle.HomePosition,
                                                new MavlinkWaypoint
                                                    {
                                                        Latitude = vehicle.Latitude,
                                                        Longitude = vehicle.Longitude,
                                                        Altitude = DefaultAltitude,
                                                        Param4 = (float) VehicleSelected.Heading
                                                    },
                                                vehicle.Tasks[0]);
                    vehicle.MissionID = VehicleSelected.Tasks[0].TaskID;
                }
            }
        }


        private void VehicleSelectedFunction(VehicleSelected obj)
        {
            if (obj.MouseClick == MouseClickEnum.Right)
            {
                MarginView = obj.Thickness;
                VisibilityView = Visibility.Visible;
                VehicleSelected = obj.Vehicle;
            }
            else
            {
                VisibilityView = Visibility.Hidden;
                if (obj.MouseClick == MouseClickEnum.Left)
                {
                //    VehicleSelected = obj.Vehicle;
                //    IsJoystickChecked = false;
                }
            }
        }

        private void CommandStateClick(object obj)
        {
            if (VehicleComponent == null)
                VehicleComponent = ServiceLocator.Current.GetInstance<IVehicleComponent>();
            if (VehicleComponent == null) return;

            if (MissionComponent == null)
                MissionComponent = ServiceLocator.Current.GetInstance<IMissionComponent>();
            if (MissionComponent == null) return;

            VisibilityView = Visibility.Hidden;
            var state = obj as string;
            switch (state)
            {
                case "CameHome":
                    if (VehicleSelected != null)
                        VehicleComponent.SendModeCommand(VehicleSelected.ID, (uint)VehicleModeEnum.RTL);
                    break;
                case "Land":
                    if (VehicleSelected != null)
                        VehicleComponent.SendModeCommand(VehicleSelected.ID, (uint)VehicleModeEnum.Land);
                    break;
              
                case "Pause":
                    if (VehicleSelected != null)
                        VehicleComponent.SendModeCommand(VehicleSelected.ID, (uint)VehicleModeEnum.Loiter);
                    break;
                case "Start":
                    if (VehicleSelected != null)
                    {
                        //VehicleComponent.SendModeCommand(VehicleSelected.ID, (uint) VehicleModeEnum.Auto);
                        //MissionComponent.StartMission(VehicleSelected.ID, 0);
                        MissionComponent.StartMissionAuto(VehicleSelected.ID, 0);
                    }
                    break;
                case "LoadMission":
                    if (VehicleSelected.Tasks.Count > 0)
                    {
                        VehicleSelected.IsActivateMission = false;
                        MissionComponent.UploadTask(VehicleSelected.ID,
                                                    VehicleSelected.HomePosition,
                                                    new MavlinkWaypoint
                                                        {
                                                            Latitude = VehicleSelected.Latitude,
                                                            Longitude = VehicleSelected.Longitude,
                                                            Altitude = DefaultAltitude,
                                                            Param4 = (float) VehicleSelected.Heading
                                                        },
                                                    VehicleSelected.Tasks[0]);
                        VehicleSelected.MissionID = VehicleSelected.Tasks[0].TaskID;
                    }
                    break;
                case "TakeOff":
                    if(VehicleSelected != null)
                        VehicleComponent.TakeOffCommand(VehicleSelected.ID, 100, (float)VehicleSelected.Latitude,
                                                        (float)VehicleSelected.Longitude, (float)VehicleSelected.Heading);
                    break;
                case "ControlRequest":
                    //if (value)
                    //    ServiceLocator.Current.GetInstance<IJoystick>(idx).Start();
                    //else
                    //    ServiceLocator.Current.GetInstance<IJoystick>().Stop();
                    break;
                case "OpenCamera":
                    
                    if (QuadPropertyWindow != null)
                    {
                        QuadPropertyWindow.Close();
                        QuadPropertyWindow = null;
                    }

                    QuadPropertyWindow = new QuadPropertyWindow();
                    QuadPropertyWindow.Show();
                    break;
            }
        }

        private bool _isJoystickChecked;
        private RelayCommand<VehicleModeEnum> _commandsActionMode;

        public QuadPropertyWindow QuadPropertyWindow { get; set; }
        public bool IsJoystickChecked
        {
            get { return _isJoystickChecked; }
            set
            {
                _isJoystickChecked = value;
                if (value)
                {
                    VehicleComponent.SendModeCommand(VehicleSelected.ID, (uint)VehicleModeEnum.Loiter);
                    ServiceLocator.Current.GetInstance<IJoystick>().Start(VehicleSelected.ID);
                }
                else
                {
                    ServiceLocator.Current.GetInstance<IJoystick>().Stop();
                }
                OnPropertyChanged();
            }
        }
       

        public Visibility VisibilityView
        {
            get { return _visibilityView; }
            set { _visibilityView = value;OnPropertyChanged(); }
        }

        public Thickness MarginView
        {
            get { return _marginView; }
            set { _marginView = value; OnPropertyChanged();}
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
