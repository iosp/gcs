using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using Common.Class;
using Common.Enums;
using GCSArduino.Enums;
using Interfaces;

namespace GCSArduino.Model
{
    public class GuiQuadVehicle :Vehicle
    {
       
        private QuadStatus _quadStatus;
        private ObservableCollection<ErrorListModel> _quasErrors;
        public VideoBase VideoBase { get; set; }

        private bool _isActivateMission;

        public GuiQuadVehicle(long id)
            : base(id)
        {
            QuadStatus = new QuadStatus();
            QuasErrors= new ObservableCollection<ErrorListModel>();
            
            MapEntity = new Entity(id) { FileName = ConfigurationManager.AppSettings["ImageDirectory"] + "Quad.png" ,Name = Name};
            MissionID = -1;
        }

        public GuiQuadVehicle(long id, string address, int port, uint handle, string fileName)
            : base(id)
        {
            //VideoBase = new VideoLanGst(address, port, handle, fileName);
            QuadStatus = new QuadStatus();
            QuasErrors = new ObservableCollection<ErrorListModel>();
            MapEntity = new Entity(id) { FileName = ConfigurationManager.AppSettings["ImageDirectory"] + "Quad.png" ,Name = Name}; 
            IsActivateMission = false;
        }
        public override bool IsActivateMission
        {
            get { return _isActivateMission; }
            set { 
                _isActivateMission = value;
                MapEntity.MissionStatus = value;
                OnPropertyChanged(); }
        }
        public void SetArm(bool arm)
        {
            MapEntity.Armed  = arm;
            QuadStatus.Armed = arm;
        }
        public void SetMode(VehicleModeEnum mode)
        {
            QuadStatus.Mode = mode;
            MapEntity.VehicleMode = mode;
        }

        public  QuadStatus QuadStatus
        {
            get { return _quadStatus; }
            set
            {
                _quadStatus = value;
                OnPropertyChanged();
            }
        }


        public ObservableCollection<ErrorListModel> QuasErrors
        {
            get { return _quasErrors; }
            set
            {
                if (Equals(value, _quasErrors)) return;
                _quasErrors = value;
                OnPropertyChanged();
            }
        }
        public override void SetHomePosition(Waypoint waypoint)
        {
            HomePosition.Latitude = waypoint.Latitude;
            HomePosition.Longitude = waypoint.Longitude;
            HomePosition.Altitude = waypoint.Altitude;
            HomePosition.Command = waypoint.Command;
        }
        public override void SetCurrentHome()
        {
            HomePosition = new MavlinkWaypoint();
            HomePosition.Latitude = Latitude;
            HomePosition.Longitude = Longitude;
            HomePosition.Altitude = Altitude;
            //HomePosition.Command = Command;
        }

        public override void TakeOffCommand( float altitude,float latitude,float longitude,float yawangle)
        {
            throw new System.NotImplementedException();
        }

       

        public override void ReadMission()
        {
            throw new System.NotImplementedException();
        }

        public override void UploadMission(IMission mission)
        {
            throw new System.NotImplementedException();
        }

        public override void UploadTask(Waypoint homePosition, Waypoint firstPosition, ITask task)
        {
            throw new System.NotImplementedException();
        }

        public override void StartMission(int missionId)
        {
            throw new System.NotImplementedException();
        }
        public override void StartMissionAuto(int missionId)
        {
            throw new System.NotImplementedException();
        }

        public override void StopMission(int missionId)
        {
            throw new System.NotImplementedException();
        }

        public override void ResumeMission(int missionId)
        {
            throw new System.NotImplementedException();
        }

        public override void PauseMission(int missionId)
        {
            throw new System.NotImplementedException();
        }

        public override void AbortMission(int missionId)
        {
            throw new System.NotImplementedException();
        }

        public override void FinishMission(int missionId, int taskId)
        {
            throw new System.NotImplementedException();
        }

        public override void DeleteMission(int missionId)
        {
            throw new System.NotImplementedException();
        }

        public override void StartTask(int missionId, int taskId)
        {
            throw new System.NotImplementedException();
        }

        public override void StopTask(int missionId, int taskId)
        {
            throw new System.NotImplementedException();
        }

        public override void ResumeTask(int missionId, int taskId)
        {
            throw new System.NotImplementedException();
        }

        public override void PauseTask(int missionId, int taskId)
        {
            throw new System.NotImplementedException();
        }

        public override void LoadTask(int missionId, int taskId)
        {
            throw new System.NotImplementedException();
        }

        public override void SkipTask(int missionId, int taskId)
        {
            throw new System.NotImplementedException();
        }

        public override void RestartMission(int missionId, int waypointId)
        {
            throw new System.NotImplementedException();
        }

        public override void GoToCurrentWaypointId(int missionId, int waypointId)
        {
            throw new System.NotImplementedException();
        }

        public override void ReturnHome()
        {
            throw new System.NotImplementedException();
        }

        public override void ManualMode()
        {
            throw new System.NotImplementedException();
        }

        public override void SendData(byte messageType, object indata)
        {
            throw new System.NotImplementedException();
        }

        public override void SendCommand(ushort command, List<float> param)
        {
            throw new System.NotImplementedException();
        }

        public override void SendModeCommand(uint mode)
        {
            throw new System.NotImplementedException();
        }

        public override void GotoWaypointCommand(Waypoint waypoint)
        {
            throw new System.NotImplementedException();
        }

        public override void SetArmDisArmCommand(bool armDisArm)
        {
            throw new System.NotImplementedException();
        }
    }
}
