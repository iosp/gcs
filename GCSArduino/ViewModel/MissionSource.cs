using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Common.Annotations;
using Common.Class;
using Common.Utils;
using GCSArduino.Enums;
using GCSArduino.Interface;
using GCSArduino.Messengers;
using GalaSoft.MvvmLight.Messaging;
using Interfaces;
using Microsoft.Practices.ServiceLocation;
using MissionPackage;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;
using SaveFileDialog = Microsoft.Win32.SaveFileDialog;


namespace GCSArduino.ViewModel
{
    public class MissionSource : IMissionSource, INotifyPropertyChanged
    {
        private IMission _mission;
        private const float DefaultAltitude = 100;
        public IMissionComponent MissionComponent { get; set; }


        public MissionSource(IMissionComponent missionComponent)
        {
            Messenger.Default.Register<MapWaypointMessenger>(this, MapWaypointMessengerAction);
            Messenger.Default.Register<TaskSelectedByShapeMessenger>(this, TaskSelectedByShapeMessengerAction);
            MissionComponent = missionComponent;
            Mission = new Mission();

            Logger.Info("MissionSource init");
        }

        public void LoadMission()
        {
            var openFileDialog = new OpenFileDialog
                {
                    InitialDirectory = ConfigurationManager.AppSettings["MissionDirectory"],
                    Filter = "xml files (*.xml)|*.xml",
                    RestoreDirectory = true
                };

            var result = openFileDialog.ShowDialog();
            if (result == true)
            {
                LoadDataSourceMission(openFileDialog.FileName, true);
            }
        }

        public void SaveMission()
        {
            var openFileDialog = new SaveFileDialog
            {
                InitialDirectory = ConfigurationManager.AppSettings["MissionDirectory"],
                Filter = "xml files (*.xml)|*.xml",
                RestoreDirectory = true
            };

            var result = openFileDialog.ShowDialog();
            if (result == true)
            {
                var save = new Xml<Mission>();
                save.SaveAs(openFileDialog.FileName, (MissionPackage.Mission)Mission);
            }

            
        }

        public void LoadDataSourceMission(string fileName, bool ifFromLoaded)
        {
            var load = new Xml<MissionPackage.Mission>();
            Mission = load.Load(fileName);
            Messenger.Default.Send(new SelectedMissionMessenger { SelectedMission = (MissionPackage.Mission)Mission });
        }
        
        public void UpdateMission(IMission mission)
        {
            Mission = mission as Mission;
            Messenger.Default.Send(new SelectedMissionMessenger { SelectedMission = (MissionPackage.Mission)Mission });
        }

        private void MapWaypointMessengerAction(MapWaypointMessenger obj)
        {
            //NavTask.NavPath.Clear();
            //ServiceLocator.Current.GetInstance<IMap>().RemoveAllShape();
            byte index = 1;
            var mission = (MissionPackage.Mission)Mission;
            ITask first = null;
            foreach (Task task in mission.Tasks)
            {
                if (task.TaskID == obj.TaskId)
                {
                    first = task;
                    break;
                }
            }
            var taskNav = first as MavlinkTaskNav;

            if (taskNav != null)
            {
                taskNav.NavPath.Clear();
             
                foreach (var waypoint in obj.Points)
                {

                    double x = waypoint.X, y = waypoint.Y;
                    //Utils.Utils.ConvertGeoToMercator(ref x, ref y);
                    taskNav.NavPath.Add(new MavlinkWaypoint
                        {
                            Id = index,
                            Command = (ushort) MissionMavCmd.WAYPOINT,
                            Latitude = y,
                            Longitude = x,
                            Altitude = DefaultAltitude,
                            Param1 = 0,
                            Param2 = 0,
                            Param3 = 0,
                            Param4 = 0
                        });
                    index++;
                }
            }
            else
            {
                var mavlinkTaskNav = new MavlinkTaskNav("Task_" + (ushort)obj.TaskId, (ushort)obj.TaskId);
                foreach (var waypoint in obj.Points)
                {
                    double x = waypoint.X, y = waypoint.Y;
                    //Utils.Utils.ConvertGeoToMercator(ref x, ref y);
                    mavlinkTaskNav.NavPath.Add(new MavlinkWaypoint
                        {
                            Id = index,
                            Command = (ushort) MissionMavCmd.WAYPOINT,
                            Latitude = y,
                            Longitude = x,
                            Altitude = DefaultAltitude,
                            Param1 = 0,
                            Param2 = 0,
                            Param3 = 0,
                            Param4 = 0
                        });
                    index++;
                }
                Mission.AddTask(mavlinkTaskNav);
            }

            Messenger.Default.Send(new UpdataMissionMessenger {MissionId = Mission.MissionID});
        }
        
        private void TaskSelectedByShapeMessengerAction(TaskSelectedByShapeMessenger obj)
        {
            var mission = (Mission)Mission;
            ITask first = mission.Tasks.FirstOrDefault(task => task.TaskID == obj.SelectedTaskId);
            var taskNav = first as MavlinkTaskNav;

            //ServiceLocator.Current.GetInstance<IListWaypoint>().NavTask = taskNav;
        }


        #region public Function
        public int GenerateTaskId()
        {
            var mission = Mission as Mission;
            if (mission.Tasks.Count == 0)
                return 1000;
            return mission.Tasks.Select(task => task.TaskID).Concat(new[] { 0 }).Max() + 1;
        }
        public void AddTask(ITask task)
        {
            Task t = null;
            var mission = Mission as Mission;
            foreach (Task task1 in mission.Tasks)
            {
                if (task1.TaskID == task.TaskID)
                {
                    t = task1;
                    break;
                }
            }
            if(t==null)
                Mission.AddTask(task);
            else
            {
                mission.Tasks.Remove(t);
                mission.Tasks.Add(task as Task);
            }
        }
        #endregion
        
        public IMission Mission
        {
            get { return _mission; }
            set
            {
                _mission = value ;
                Messenger.Default.Send(new UpdataMissionMessenger {MissionId = value.MissionID});
                
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
