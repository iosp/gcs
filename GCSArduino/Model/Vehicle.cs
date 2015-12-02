using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Common.Annotations;
using Common.Class;
using Interfaces;
using MissionPackage;

namespace GCSArduino.Model
{
    public abstract class Vehicle : IVehicle,  INotifyPropertyChanged
    {
        private Entity _mapEntity;

        private long _id;
        private double _latitude;
        private double _longitude;
        private int _altitude;
        private double _heading;
        private int _roll;
        private double _pitch;
        private Waypoint _homePosition;
        private ushort _nextWaypoint;
        private ushort _distanceFromTarget;
        private ObservableCollection<Waypoint> _waypoints;
        private string _name;
        
        private string _taskName;
        private ObservableCollection<ITask> _tasks;
        private bool _isSelected;
        private int _missionId;
        
        private bool _selected;
        private ITask _taskID;
        private float _timeInWaypoint;
        private bool _missionReady;
        private JoystickModel _joystickIn;


        protected Vehicle(long id)
        {
          
            ID = id;
            
            var x1 = (id & 0x000000FF) >> 0;
	        var x2 = (id & 0x0000FF00) >> 8;
	        var x3 = (id & 0x00FF0000) >> 16;
	        var x4 = (id & 0xFF000000) >> 24;
            Name = "כלי_" + x1 + "." + x2 + "." + x3 + "." + x4 ;
            Tasks = new ObservableCollection<ITask>();
            //IsActivateMission = false;
            JoystickIn = new JoystickModel();
        }

       
        public int MissionID
        {
            get { return _missionId; }
            set
            {
                _missionId = value;
                if (MapEntity != null)
                MapEntity.MissionId = value;
                OnPropertyChanged();
            }
        }

        public abstract bool IsActivateMission { get; set; }

        public JoystickModel JoystickIn
        {
            get { return _joystickIn; }
            set { _joystickIn = value; OnPropertyChanged();}
        }

       

        public ITask Task
        {
            get { return _taskID; }
            set { _taskID = value; OnPropertyChanged();}
        }

        public long ID
        {
            get { return _id; }
            set
            {
                if (value == _id) return;
                _id = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Waypoint> Waypoints
        {
            get
            {
                if (_tasks.Count > 0)
                {
                    var t = _tasks[0] as MavlinkTaskNav;
                    return t.NavPath;
                }
                return null;
            }
            
        }

       
        public ObservableCollection<ITask> Tasks
        {
            get { return _tasks; }
            set { _tasks = value;OnPropertyChanged(); }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value;OnPropertyChanged(); }
        }
       

        public double Heading
        {
            get { return _heading; }
            set
            {
                _heading = value;
                MapEntity.Heading = value;
                OnPropertyChanged();
            }
        }

        public int Roll
        {
            get { return _roll; }
            set
            {
                _roll = value;
                OnPropertyChanged();
            }
        }

        public double Pitch
        {
            get { return _pitch; }
            set
            {
                _pitch = value;
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
                //MapEntity.YLocation = value;
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
                //MapEntity.XLocation = value;
                OnPropertyChanged();
            }
        }

        //public bool Selected
        //{
        //    get { return _selected; }
        //    set
        //    {
        //        _selected = value;
        //        MapEntity.IsSelected = value;
        //        OnPropertyChanged();
        //    }
        //}

        public int Altitude
        {
            get { return _altitude; }
            set
            {
                if (value == _altitude) return;
                _altitude = value;
                
                OnPropertyChanged();
            }
        }

        public float TimeInWaypoint
        {
            get { return _timeInWaypoint; }
            set
            {
                _timeInWaypoint = value; 
                OnPropertyChanged();
                MapEntity.TimeInWaypoint = value;
            }
        }

        public Waypoint HomePosition
        {
            get { return _homePosition; }
            set { _homePosition = value;OnPropertyChanged(); }
        }

        //public ObservableCollection<Waypoint> Waypoints
        //{
        //    get { return _waypoints; }
        //    set { _waypoints = value;OnPropertyChanged(); }
        //}

        public ushort NextWaypoint
        {
            get { return _nextWaypoint; }
            set { _nextWaypoint = value; OnPropertyChanged(); }
        }

        public ushort DistanceFromTarget
        {
            get { return _distanceFromTarget; }
            set { _distanceFromTarget = value; OnPropertyChanged(); }
        }

        public bool IsSelected
        {
            get { return MapEntity.IsSelected; }
            set { MapEntity.IsSelected = value; OnPropertyChanged(); }
        }

        public abstract void SetHomePosition(Waypoint waypoint);
        public abstract void SetCurrentHome();
        public abstract void TakeOffCommand (float altitude, float latitude, float longitude, float yawangle);
        public abstract void ReadMission();
        public abstract void UploadMission(IMission mission);
        public abstract void UploadTask(Waypoint homePosition, Waypoint firstPosition, ITask task);
        public abstract void StartMission(int missionId);
        public abstract void StartMissionAuto(int missionId);
        public abstract void StopMission(int missionId);
        public abstract void ResumeMission(int missionId);
        public abstract void PauseMission(int missionId);
        public abstract void AbortMission(int missionId);
        public abstract void FinishMission(int missionId, int taskId);
        public abstract void DeleteMission(int missionId);
        public abstract void StartTask(int missionId, int taskId);
        public abstract void StopTask(int missionId, int taskId);
        public abstract void ResumeTask(int missionId, int taskId);
        public abstract void PauseTask(int missionId, int taskId);
        public abstract void LoadTask(int missionId, int taskId);
        public abstract void SkipTask(int missionId, int taskId);
        public abstract void RestartMission(int missionId, int waypointId);
        public abstract void GoToCurrentWaypointId(int missionId, int waypointId);

        public abstract void ReturnHome();
        public abstract void ManualMode();
        public abstract void SendData(byte messageType, object indata);
        public abstract void SendCommand(ushort command, List<float> param);
        public abstract void SendModeCommand(uint mode);
        public abstract void GotoWaypointCommand(Waypoint waypoint);
        public abstract void SetArmDisArmCommand(bool armDisArm);

        public Entity MapEntity
        {
            get { return _mapEntity; }
            set
            {
                _mapEntity = value;
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
