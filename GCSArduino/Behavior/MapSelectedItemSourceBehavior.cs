using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Threading;
using Common.Enums;
using ESRI.ArcGIS.Client;
using System.Windows.Interactivity;
using ESRI.ArcGIS.Client.Geometry;
using ESRI.ArcGIS.Client.Symbols;
using GCSArduino.Messengers;
using GCSArduino.Model;
using GalaSoft.MvvmLight.Messaging;
using Interfaces;
using Microsoft.Practices.ServiceLocation;

namespace GCSArduino.Behavior
{
    public class MapSelectedItemSourceBehavior : Behavior<Map>
    {
        private bool _applied;
        public string ItemLayerId { get; set; }
        public string BitmapLayerId { get; set; }
        public string XPropertyPath { get; set; }
        public string YPropertyPath { get; set; }
       
        public string EnvelopePropertyPath { get; set; }
        public string ImageSourcePropertyPath { get; set; }


        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register("SelectedItem", typeof(object), typeof(MapSelectedItemSourceBehavior), new PropertyMetadata(OnSelectedItemChanged));

        public object SelectedItem
        {
            get { return GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        public static readonly DependencyProperty SymbolProperty =
            DependencyProperty.Register("Symbol", typeof(Symbol), typeof(MapSelectedItemSourceBehavior));

        public Symbol Symbol
        {
            get { return (Symbol)GetValue(SymbolProperty); }
            set { SetValue(SymbolProperty, value); }
        }

        protected override void OnAttached()
        {
            AssociatedObject.Loaded += AssociatedObject_Loaded;
            AssociatedObject.PanDuration = TimeSpan.Zero;
            AssociatedObject.ZoomDuration = TimeSpan.Zero;
        }

        void AssociatedObject_Loaded(object sender, RoutedEventArgs e)
        {
            _applied = true;
            Init();
            ChangeObject(null, SelectedItem as INotifyPropertyChanged);
            
        }

        protected override void OnDetaching()
        {
            AssociatedObject.Loaded -= AssociatedObject_Loaded;
            _applied = false;
            Remove();
        }

        private static void OnSelectedItemChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var me = (MapSelectedItemSourceBehavior)sender;
            if (me._applied)
            {
                me.ChangeObject(e.OldValue as INotifyPropertyChanged, e.NewValue as INotifyPropertyChanged);
            }
        }

        private void ChangeObject(INotifyPropertyChanged oldObj, INotifyPropertyChanged newObj)
        {
            var newData = new ObservableCollection<object>();
            // _data.Clear();
            if (oldObj != null && newObj != null)
            {
                newObj.PropertyChanged -= WhenObjectPropertyChanged;
            }
            if (SelectedItem != null)
            {
                newData.Add(SelectedItem);
                ApplyEnvelope(SelectedItem);
                ApplyImage(SelectedItem);
            }
            if (newObj != null)
            {
                newObj.PropertyChanged += WhenObjectPropertyChanged;
            }
            _dataSource.ItemsSource = newData;
        }

        void WhenObjectPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == EnvelopePropertyPath)
            {
                ApplyEnvelope(sender);
            }
            else if (e.PropertyName == ImageSourcePropertyPath)
            {
                ApplyImage(sender);
            }
        }

        private void ApplyImage(object obj)
        {
            if (!String.IsNullOrEmpty(ImageSourcePropertyPath))
            {
                var entity = obj as Entity;
                if (entity != null)
                {
                    var graphicsLayer = AssociatedObject.Layers["Frame"] as GraphicsLayer;
                    if (graphicsLayer != null)
                    {
                        var symbol =
                            graphicsLayer.Graphics[0].Symbol as
                                ESRI.ArcGIS.Client.FeatureService.Symbols.PictureMarkerSymbol;

                        if (entity.ViewPort != null)
                            graphicsLayer.Graphics[0].Geometry = new MapPoint((entity.ViewPort.XMax + entity.ViewPort.XMin) / 2, (entity.ViewPort.YMax + entity.ViewPort.YMin) / 2);

                        var imgProp = obj.GetType().GetProperty(ImageSourcePropertyPath);

                        var img = imgProp.GetValue(obj) as ImageSource;
                        if (symbol != null)
                        {
                            symbol.Width = 500;
                            symbol.Height = 500;
                            symbol.Source = img;
                        }
                    }
                }

            }
        }

        private void ApplyEnvelope(object obj)
        {
            if (!String.IsNullOrEmpty(EnvelopePropertyPath))
            {
                var envProp = obj.GetType().GetProperty(EnvelopePropertyPath);
                if (envProp != null)
                {
                    var env = envProp.GetValue(obj) as Envelope;
                    if (env != null)
                    {

                        //ElementLayer.SetEnvelope(_bitMapFrame, env);
                        //AssociatedObject.Layers.First().SetValue(ElementLayer.EnvelopeProperty,env);
                        AssociatedObject.ZoomTo(env);
                        AssociatedObject.Rotation = ((Entity)SelectedItem).Heading * -1;
                    }
                }
            }
        }

        private PointDataSource _dataSource;
        private ObservableCollection<object> _data;


        private void Init()
        {
            try
            {
                var bitmapLayer = AssociatedObject.Layers.FirstOrDefault(l => !String.IsNullOrWhiteSpace(l.ID) && l.ID.Equals(BitmapLayerId)) as GraphicsLayer;

                if (!String.IsNullOrWhiteSpace(BitmapLayerId))
                {
                    if (bitmapLayer != null)
                        AssociatedObject.Layers.Remove(bitmapLayer);
                    bitmapLayer = new GraphicsLayer { ID = BitmapLayerId };
                    var graphic = new Graphic { Symbol = new ESRI.ArcGIS.Client.FeatureService.Symbols.PictureMarkerSymbol() };


                    bitmapLayer.Graphics.Add(graphic);

                    AssociatedObject.Layers.Add(bitmapLayer);
                }


                _data = new ObservableCollection<object>();
                _dataSource = new PointDataSource
                {
                    XCoordinateBinding = new Binding(XPropertyPath),
                    YCoordinateBinding = new Binding(YPropertyPath),
                   
                    ItemsSource = _data,
                    DataSpatialReference = new SpatialReference(Esri.GCS_WGS_1984)
                };

                var layer = AssociatedObject.Layers.FirstOrDefault(l => !String.IsNullOrWhiteSpace(l.ID) && l.ID.Equals(ItemLayerId)) as GraphicsLayer;

                if (Symbol != null && !String.IsNullOrWhiteSpace(ItemLayerId))
                {
                    if (layer != null) AssociatedObject.Layers.Remove(layer);
                    layer = new GraphicsLayer
                    {
                        Renderer = new SimpleRenderer { Symbol = Symbol },
                        GraphicsSource = _dataSource,
                        ID = ItemLayerId
                    };

                    AssociatedObject.Layers.Add(layer);

                }
                else if (layer != null)
                    AssociatedObject.Layers.Remove(layer);
            }
            catch (Exception exception)
            {
                
            }
            AssociatedObject.MouseDown += AssociatedObject_MouseDown;
            GraphicsLayer graphicsLayer = AssociatedObject.Layers["Entities"] as GraphicsLayer;
            graphicsLayer.MouseLeftButtonDown += graphicsLayer_MouseLeftButtonDown;
            graphicsLayer.MouseRightButtonDown += graphicsLayer_MouseRightButtonDown;
           
          
        }

        public bool IsGraphicsLayerMouseButton = false;
        void AssociatedObject_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (IsGraphicsLayerMouseButton)
                IsGraphicsLayerMouseButton = false;
            else
            {
                Messenger.Default.Send(new VehicleSelected
                {
                    MouseClick = MouseClickEnum.None,
                });   
            }
        }

        void graphicsLayer_MouseRightButtonDown(object sender, GraphicMouseButtonEventArgs e)
        {
            var entity = e.Graphic.Attributes.Values.First() as Entity;
            if (entity != null)
            {
                var point = e.GetPosition(AssociatedObject);

                IVehicle  vehicleSelected = null;
                foreach (var vehicle in ServiceLocator.Current.GetInstance<IVehiclesSource>().Vehicles.Where(vehicle => vehicle.ID == entity.Id))
                {
                    vehicleSelected = vehicle as GuiQuadVehicle;
                }

                Messenger.Default.Send(new VehicleSelected
                    {
                        Vehicle = vehicleSelected,
                        MouseClick = MouseClickEnum.Right,
                        Thickness = new Thickness(point.X - 100, point.Y,0, 0),
                    });
            }
            IsGraphicsLayerMouseButton = true;
        }

        void graphicsLayer_MouseLeftButtonDown(object sender, GraphicMouseButtonEventArgs e)
        {
           
            var entity = e.Graphic.Attributes.Values.First() as Entity;

            if (entity != null)
            {
                IVehicle vehicleSelected = null;
                foreach (var vehicle in ServiceLocator.Current.GetInstance<IVehiclesSource>().Vehicles.Where(vehicle => vehicle.ID == entity.Id))
                {
                    vehicleSelected = vehicle as GuiQuadVehicle;
                }
                Messenger.Default.Send(new VehicleSelected { Vehicle = vehicleSelected, MouseClick = MouseClickEnum.Left });
                var graphic = entity as Graphic;
                //graphic.
               //Application.Current.Dispatcher.Invoke(new Action(() => { graphic.Select(); }));
            }
        }

        private void Remove()
        {
            var layer = AssociatedObject.Layers.FirstOrDefault(l => l.ID.Equals(ItemLayerId));
            if (layer != null) AssociatedObject.Layers.Remove(layer);
        }
    }

}
