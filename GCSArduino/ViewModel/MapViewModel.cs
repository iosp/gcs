using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Windows;
using System.Windows.Threading;
using Common.Class;
using Common.Utils;
using ESRI.ArcGIS.Client;
using ESRI.ArcGIS.Client.Symbols;
using GCSArduino.Interface;
using GCSArduino.Messengers;
using GCSArduino.Model;
using GalaSoft.MvvmLight.Messaging;
using MissionPackage;

namespace GCSArduino.ViewModel
{
    public class MapViewModel : BaseViewModel , IMap 
    {
        private ObservableCollection<Shape> _itemsSource;
        private ObservableCollection<Shape> _shapesItemsSource;
        private ObservableCollection<Entity> _entitysItemsSource;
        private Entity _selectedEntity;
        private Shape _selectedShape;
        private string _filePath;
        private DrawMode _drawMode;
        private ushort _newTaskShapeId;
        private EditGeometry.Action _editGeometryAction;

        public MapViewModel(IMenuViewModel menuViewModel)
        {
            ItemsSource = new ObservableCollection<Shape>();
            EntitysItemsSource = new ObservableCollection<Entity>();
            ShapesItemsSource = new ObservableCollection<Shape>();

            FilePath = ConfigurationManager.AppSettings["Map"];
            //FilePath = "http://server.arcgisonline.com/arcgis/rest/services/World_Imagery/MapServer";
          
            DrawMode = DrawMode.None;

            Messenger.Default.Register<SelectedMissionMessenger>(this, SelectedMissionMessengerFunction);
            Messenger.Default.Register<VehicleSelected>(this, VehicleSelectedAction);
            Messenger.Default.Register<MapMouseRightButtonDownMessenger>(this, MapMouseRightButtonDownMessengerFunction);
            Logger.Info("MapViewModel init");
        }

        private void MapMouseRightButtonDownMessengerFunction(MapMouseRightButtonDownMessenger obj)
        {
            
        }

        private void VehicleSelectedAction(VehicleSelected obj)
        {
            if(obj.Vehicle == null) return;
            foreach (var entity in EntitysItemsSource)
            {
                if (entity.Id == obj.Vehicle.ID)
                {
                    SelectedEntity = entity;
                    //Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => SelectedEntity.Selected = true));
                    break;
                }
            }
            
        }

        private void SelectedMissionMessengerFunction(SelectedMissionMessenger obj)
        {
            RemoveAllShape();
            foreach (var task in obj.SelectedMission.Tasks)
            {
                var taskNav = task as MavlinkTaskNav;
                if (taskNav != null)
                {
                    var waypoints = new List<Waypoint>();

                    foreach (var waypoint in taskNav.NavPath)
                    {
                        if (waypoint.Command == (decimal) MMAVLink.MAVLink.MAV_CMD.WAYPOINT)
                        {
                            var x = waypoint.Latitude;
                            var y = waypoint.Longitude;
                            //Utils.Utils.ConvertMercatorToGeo(ref y, ref x);
                            waypoints.Add(new MavlinkWaypoint
                                {
                                    Latitude = x,
                                    Longitude = y,
                                    Altitude = waypoint.Altitude,
                                    Id = waypoint.Id,
                                    Command = waypoint.Command
                                });
                        }
                    }
                    var newShape = new Shape(taskNav.TaskID, waypoints) { Symbol = (LineSymbol)Application.Current.FindResource("LineSymbol") };
                    
                    AddShape(newShape);
                }
            }
        }

        public ObservableCollection<Shape> ItemsSource
        {
            get { return _itemsSource; }
            set
            {
                if (Equals(value, _itemsSource)) return;
                _itemsSource = value;
                OnPropertyChanged();
            }
        }

        public void AddShape(Shape shape)
        {
            var tempShape = ShapesItemsSource.FirstOrDefault(s => s.ID == shape.ID);
            if (tempShape != null)
            {
                EditGeometryAction = EditGeometry.Action.EditCanceled;
                ShapesItemsSource.Remove(tempShape);
            }
            ShapesItemsSource.Add(shape);

        }
        
        public void RemoveShape(int shapeId)
        {
            Shape temp = ShapesItemsSource.FirstOrDefault(s => s.ID == shapeId);
            if (temp != null)
                ShapesItemsSource.Remove(temp);
        }

        public void RemoveAllShape()
        {
            if (ShapesItemsSource != null && ShapesItemsSource.Count>0)
            ShapesItemsSource.Clear();
        }
        public ObservableCollection<Shape> ShapesItemsSource
        {
            get { return _shapesItemsSource; }
            set { _shapesItemsSource = value; OnPropertyChanged(); }
        }
        public void SelectedShapesItemsSource(int taskId)
        {
            Shape tempShape = null;
            foreach (var shape in ShapesItemsSource)
            {
                if (shape.ID == taskId)
                    tempShape = shape;
            }
            if (tempShape != null)
                SelectedShape = tempShape;
        }
        public ObservableCollection<Entity> EntitysItemsSource
        {
            get { return _entitysItemsSource; }
            set { _entitysItemsSource = value; OnPropertyChanged(); }
        }

        public Entity SelectedEntity
        {
            get { return _selectedEntity; }
            set { _selectedEntity = value; OnPropertyChanged(); }
        }

        public Shape SelectedShape
        {
            get { return _selectedShape; }
            set { _selectedShape = value; OnPropertyChanged(); }
        }

        public string FilePath
        {
            get { return _filePath; }
            set { _filePath = value;OnPropertyChanged(); }
        }

        public DrawMode DrawMode
        {
            get { return _drawMode; }
            set { _drawMode = value; OnPropertyChanged();}
        }

        public ushort NewTaskShapeId
        {
            get { return _newTaskShapeId; }
            set { _newTaskShapeId = value; OnPropertyChanged(); }
        }

        public EditGeometry.Action EditGeometryAction
        {
            get { return _editGeometryAction; }
            set { _editGeometryAction = value; OnPropertyChanged();}
        }
    }
}
