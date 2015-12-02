using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using Common.Annotations;
using Common.Class;
using Common.Utils;
using ESRI.ArcGIS.Client;
using GCSArduino.Interface;
using GCSArduino.Messengers;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Interfaces;
using Microsoft.Practices.ServiceLocation;
using MissionPackage;


namespace GCSArduino.ViewModel
{
    public class ListWaypointViewModel : INotifyPropertyChanged, IListWaypoint
    {
        private Waypoint _selectedWaypoint;
        private Task _taskNav;

        public Waypoint SelectedWaypoint
        {
            get { return _selectedWaypoint; }
            set
            {
                if (Equals(value, _selectedWaypoint)) return;
                _selectedWaypoint = value;
                OnPropertyChanged();
            }
        }

        public Task TaskNav
        {
            get { return _taskNav; }
            set { _taskNav = value; OnPropertyChanged();}
        }

        public IMissionSource MissionSource { get; set; }
        
        public ListWaypointViewModel(IMissionSource missionSource)
        {
            MissionSource = missionSource;
            Messenger.Default.Register<TaskSelectedByShapeMessenger>(this, TaskSelectedByShapeMessengerAction);
            
        }
        
        private void TaskSelectedByShapeMessengerAction(TaskSelectedByShapeMessenger obj)
        {
            var mission = (Mission)MissionSource.Mission;
            ITask first = mission.Tasks.FirstOrDefault(task => task.TaskID == obj.SelectedTaskId);
            TaskNav = first as MavlinkTaskNav;

            //ServiceLocator.Current.GetInstance<IListWaypoint>().NavTask = taskNav;
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

