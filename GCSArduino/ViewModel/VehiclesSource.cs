using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Threading;
using CommService;
using Common.Annotations;
using Common.Class;
using Common.Enums;
using Common.Utils;
using ESRI.ArcGIS.Client.Symbols;
using GCSArduino.Interface;
using GCSArduino.Messengers;
using GCSArduino.Model;
using GalaSoft.MvvmLight.Messaging;
using Interfaces;
using Microsoft.Practices.ServiceLocation;
using MissionPackage;

namespace GCSArduino.ViewModel
{
    public class VehiclesSource : IVehiclesSource , INotifyPropertyChanged
    {
      
        public IMap Map { get; set; }
        public IMissionComponent MissionComponent { get; set; }
        public IMissionSource MissionSource { get; set; }
        
        public VehiclesSource(IMap map, IMissionSource missionSource)
        {
            Map = map;
            MissionSource = missionSource;
            Vehicles = new ObservableCollection<IVehicle>();
            Messenger.Default.Register<VehicleSelected>(this, VehicleSelectedAction);
            Logger.Info("VehiclesSource init");
        }

        private void VehicleSelectedAction(VehicleSelected obj)
        {
            if (obj.Vehicle == null) return;
            VehicleSelected = obj.Vehicle;
        }

        public void SystemStatusServiceAction(SystemStatusService obj)
        {
            if (Vehicles != null)
            {
                var vehicle = Vehicles.FirstOrDefault(v => v.ID == obj.VehicleId) as GuiQuadVehicle ??
                              AddVehicle(obj.VehicleId);

                vehicle.SetArm(obj.Armed);
                vehicle.SetMode(obj.Mode);

                vehicle.QuadStatus.BatteryVoltage = obj.BatteryVoltage;
                vehicle.QuadStatus.BatteryRemaining = obj.BatteryRemaining;
                vehicle.QuadStatus.BatteryCurrent = obj.BatteryCurrent;
                vehicle.QuadStatus.Landed = obj.Landed;
                vehicle.QuadStatus.Failsafe = obj.Failsafe;
                vehicle.QuadStatus.ErrorQuad.Gps = obj.Gps;
                vehicle.QuadStatus.ErrorQuad.Gyro = obj.Gyro;
                vehicle.QuadStatus.ErrorQuad.Accel = obj.Accel;
                vehicle.QuadStatus.ErrorQuad.Compass = obj.Compass;
                vehicle.QuadStatus.ErrorQuad.Baro = obj.Baro;
                vehicle.QuadStatus.ErrorQuad.OptFlow = obj.OptFlow;
                vehicle.QuadStatus.ErrorQuad.Rc = obj.Rc;
                vehicle.QuadStatus.ErrorQuad.Battery = obj.Battery;
                vehicle.QuadStatus.ErrorQuad.Hdop = obj.Hdop;
                vehicle.QuadStatus.ErrorQuad.Satellites = obj.Satellites;
            }
        }

        public void VehicleStatusServiceAction(VehicleStatusService obj)
        {
            if (Vehicles != null)
            {
                var vehicle = Vehicles.FirstOrDefault(v => v.ID == obj.VehicleId) as GuiQuadVehicle;

                if (vehicle != null)
                {
                    vehicle.SetArm(obj.Armed);
                    vehicle.SetMode(obj.Mode);
                    vehicle.QuadStatus.Failsafe = obj.Failsafe;
                    vehicle.QuadStatus.Landed = obj.Landed;
                    //vehicle.QuadStatus.LinqQuality = obj.LinqQuality;
                    vehicle.QuadStatus.TickCount = obj.TickCount;
                    //Messenger.Default.Send(new VehicleSelected { Vehicle = vehicle });
                }
            }
        }

        public void SensorsError(string text, DateTime dateTime, ErrorLevelEnum errorLevel)
        {
            //vehicle.QuasErrors.Add(new ErrorListModel { Text = "Bad GPS Health", DateTime = DateTime.Now, ErrorLevel = ErrorLevelEnum.Hight });
            //System.Diagnostics.Debug.WriteLine(dateTime +"-"+ text);
        }

        public void ReadMission(long idx, byte sysid, byte compid, ObservableCollection<Waypoint> waypoints)
        {

            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() =>
                {
                    var vehicle = Vehicles.FirstOrDefault(v => v.ID == idx) as GuiQuadVehicle;
                    if (vehicle != null)
                    {
                        //vehicle.Tasks.Clear();
                        var taskId = ServiceLocator.Current.GetInstance<IMissionSource>().GenerateTaskId();
                        var mavlinkTaskNav = new MavlinkTaskNav("Task" + taskId, taskId)
                            {
                                NavPath = waypoints
                            };
                        ServiceLocator.Current.GetInstance<IMissionSource>().AddTask(mavlinkTaskNav);
                        vehicle.Tasks.Add(mavlinkTaskNav);
                        vehicle.MissionID = mavlinkTaskNav.TaskID;
                        var convertPoint = new ObservableCollection<Waypoint>();

                        foreach (MavlinkWaypoint point in mavlinkTaskNav.NavPath)
                        {
                            if (point.Command == (decimal) MMAVLink.MAVLink.MAV_CMD.WAYPOINT)
                            {
                                var x = point.Latitude;
                                var y = point.Longitude;
                                //Utils.Utils.ConvertMercatorToGeo(ref y, ref x);
                                convertPoint.Add(new MavlinkWaypoint
                                    {
                                        Latitude = x,
                                        Longitude = y,
                                        Altitude = point.Altitude,
                                        Id = point.Id,
                                        Command = point.Command,
                                        Param1 = point.Param1,
                                        Param2 = point.Param2,
                                        Param3 = point.Param3,
                                        Param4 = point.Param4,
                                        
                                    });
                            }
                        }
                        var newShape = new Shape(mavlinkTaskNav.TaskID, convertPoint)
                            {
                                Symbol = (LineSymbol) Application.Current.FindResource("LineSymbol")
                            };
                        ServiceLocator.Current.GetInstance<IMap>().AddShape(newShape);
                    }
                }));

        }

        public void SetActivateMission(long idx, bool isActivateMission)
        {
            var vehicle = Vehicles.FirstOrDefault(v => v.ID == idx) as GuiQuadVehicle;
            if (vehicle != null)
                vehicle.IsActivateMission = true;
        }

        public void SetHudData(HudDataService hudData)
        {
            var vehicle = Vehicles.FirstOrDefault(v => v.ID == hudData.VehicleId) as GuiQuadVehicle;
            if (vehicle != null)
            {
                vehicle.QuadStatus.GroundSpeed = hudData.GroundSpeed;
                vehicle.QuadStatus.AirSpeed = hudData.AirSpeed;
                vehicle.QuadStatus.Climb = hudData.Climb;
            }
        }

        public void SetHomePosition(long idx, Waypoint homeWaypoint)
        {
            var vehicle = Vehicles.FirstOrDefault(v => v.ID == idx) as GuiQuadVehicle;
            if (vehicle != null)
            {
                vehicle.HomePosition = homeWaypoint;
               
            }
        }

        public void JoystickInStatusAction(JoystickInStatusService obj)
        {
            var vehicle = Vehicles.FirstOrDefault(v => v.ID == obj.VehicleId) as GuiQuadVehicle;
            if (vehicle != null)
            {
                vehicle.JoystickIn.Roll     = obj.Chan1Raw;
                vehicle.JoystickIn.Pitch    = obj.Chan2Raw;
                vehicle.JoystickIn.Throttle = obj.Chan3Raw;
                vehicle.JoystickIn.Rudder   = obj.Chan4Raw;
            }
        }

        public void GpsRawDataServiceAction(GpsRawDataService obj)
        {
            if (Vehicles != null)
            {
                var vehicle = Vehicles.FirstOrDefault(v => v.ID == obj.VehicleId) as GuiQuadVehicle;

                if (vehicle != null)
                {
                    vehicle.Latitude = obj.Latitude;
                    vehicle.Longitude = obj.Longitude;
                    vehicle.Altitude = obj.Altitude;
                    vehicle.QuadStatus.GpsStatus = obj.GpsStatus;
                    vehicle.QuadStatus.Satellites = obj.Satellites;
                    vehicle.QuadStatus.Hdop = obj.Hdop;

                    if (obj.HomeAltitude == 0 && obj.HomeLatitude == 0 && obj.HomeLongitude == 0)
                        vehicle.HomePosition = null;

                    if (vehicle.HomePosition == null)
                    {
                        vehicle.HomePosition = new MavlinkWaypoint
                            {
                                Latitude = obj.HomeLatitude,
                                Longitude = obj.HomeLongitude,
                                Altitude = obj.HomeAltitude
                            };
                    }
                }
            }
        }

        public void GlobalPositionServiceAction(GlobalPositionService obj)
        {
            if (Vehicles != null)
            {
                var vehicle = Vehicles.FirstOrDefault(v => v.ID == obj.VehicleId) as GuiQuadVehicle;
                if (vehicle != null)
                {
                    vehicle.Latitude = obj.Latitude;
                    vehicle.Longitude = obj.Longitude;
                    vehicle.Altitude = obj.Altitude;
                    vehicle.QuadStatus.AltitudeAboveGround = obj.AltitudeAboveGround;
                    var x = vehicle.Longitude;
                    var y = vehicle.Latitude;
                    vehicle.MapEntity.Latitude = vehicle.Latitude;
                    vehicle.MapEntity.Longitude = vehicle.Longitude;
                    vehicle.MapEntity.Altitude = vehicle.Altitude;

                    //Utils.Utils.ConvertMercatorToGeo(ref x, ref y);

                    vehicle.MapEntity.XLocation = x;
                    vehicle.MapEntity.YLocation = y;
                    //Messenger.Default.Send(new VehicleSelected { VehicleId = obj.VehicleId });
                }
               
            }
        }

        public void AttitudeServiseAction(AttitudeServise obj)
        {
            if (Vehicles != null)
            {
                var vehicle = Vehicles.FirstOrDefault(v => v.ID == obj.VehicleId) as GuiQuadVehicle;
                if (vehicle != null)
                {
                    vehicle.Heading = obj.Heading;
                    vehicle.Pitch = obj.Pitch;
                    vehicle.Roll = obj.Roll;
                    vehicle.MapEntity.Heading = vehicle.Heading;
                }
            }
        }
        private ObservableCollection<IVehicle> _vehicles;
        public ObservableCollection<IVehicle> Vehicles
        {
            get { return _vehicles; }
            set
            {
                if (Equals(value, _vehicles)) return;
                _vehicles = value;
                OnPropertyChanged();
            }
        }

        private IVehicle _vehicleSelected;
        public IVehicle VehicleSelected
        {
            get { return _vehicleSelected; }
            set { _vehicleSelected = value; OnPropertyChanged(); }
        }

        private GuiQuadVehicle AddVehicle(long vehicleId)
        {
            foreach (var vehicle in Vehicles.Where(vehicle => vehicle.ID == vehicleId))
            {
                return vehicle as GuiQuadVehicle;
            }
           
            var newVehicle = new GuiQuadVehicle(vehicleId);
           
            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() =>
                {
                    bool find = Vehicles.Any(vehicle => vehicle.ID == vehicleId);
                    if (!find)
                    {
                        Vehicles.Add(newVehicle);
                        Map.EntitysItemsSource.Add(newVehicle.MapEntity);
                        /*if (MissionComponent == null)
                            MissionComponent = ServiceLocator.Current.GetInstance<IMissionComponent>();
                        if (MissionComponent != null)
                            MissionComponent.ReadMission(vehicleId);*/
                    }
                }));

              Messenger.Default.Send(new NewVehicleMessenger { Vehicle = vehicleId });
              return newVehicle;
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
