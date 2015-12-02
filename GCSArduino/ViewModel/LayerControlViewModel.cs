using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Common.Annotations;
using Common.Utils;
using GCSArduino.Interface;
using GCSArduino.Messengers;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Interfaces;
using Microsoft.Practices.ServiceLocation;
using MissionPackage;

namespace GCSArduino.ViewModel
{
    public class LayerControlViewModel :INotifyPropertyChanged, ILayerControl
    {
        

        public IVehiclesSource VehiclesSource { get; set; }
        public IMissionSource MissionSource { get; set; }
        public RelayCommand SaveMissionCommand { get; set; }
        public RelayCommand OpenMissionCommand { get; set; }

        public LayerControlViewModel(IMissionSource missionSource)
        {
            MissionSource = missionSource;
            Messenger.Default.Register<NewVehicleMessenger>(this, NewVehicleMessengerFunction); 

            SaveMissionCommand = new RelayCommand(SaveMissionCommandExecute );
            OpenMissionCommand = new RelayCommand(OpenMissionCommandExecute);

            MissionOrVehicles = true;
            Mission = MissionSource.Mission as Mission;
            //Tasks = Mission.Tasks;
            //Tasks.Add(new MavlinkTaskNav("xx",1000));

            Messenger.Default.Register<UpdataMissionMessenger>(this, UpdataMissionMessengerMessenger);
            
        }

        private void UpdataMissionMessengerMessenger(UpdataMissionMessenger obj)
        {
            OnPropertyChanged("Tasks");
        }
        

        private void NewVehicleMessengerFunction(NewVehicleMessenger obj)
        {
            if (VehiclesSource == null)
                VehiclesSource = ServiceLocator.Current.GetInstance<IVehiclesSource>();
            Vehicles = VehiclesSource.Vehicles;
        }
        
        #region private Commands Function 
        private void OpenMissionCommandExecute()
        {
            MissionSource.LoadMission();
        }

        private void SaveMissionCommandExecute()
        {
            MissionSource.SaveMission();
        }
        #endregion

        #region Property
        private ObservableCollection<IVehicle> _vehicles;
        private bool _missionOrVehicles;
        private Mission _mission;
        private ObservableCollection<Task> _tasks;
        private Task _selectedTask;

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

        public Mission Mission
        {
            get { return _mission; }
            set { _mission = value;OnPropertyChanged(); }
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

        public bool MissionOrVehicles
        {
            get { return _missionOrVehicles; }
            set { _missionOrVehicles = value; OnPropertyChanged(); }
        }

        public Task SelectedTask
        {
            get { return _selectedTask; }
            set { 
                _selectedTask = value; 
                ServiceLocator.Current.GetInstance<IMap>().SelectedShapesItemsSource(SelectedTask.TaskID);
                OnPropertyChanged();
            }
        }

        #endregion
      

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
