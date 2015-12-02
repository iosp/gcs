using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using ESRI.ArcGIS.Client;
using ESRI.ArcGIS.Client.Geometry;
using System.Windows.Interactivity;
using ESRI.ArcGIS.Client.Symbols;
using GCSArduino.Model;
using Interfaces;
using Microsoft.Practices.ServiceLocation;

namespace GCSArduino.Behavior
{
    public class MapItemsSourceBehavior : Behavior<Map>
    {
        private bool _applied;
        public string ID { get; set; }
        public string XPropertyPath { get; set; }
        public string YPropertyPath { get; set; }
        public string SelectedPropertyPath { get; set; }
        public string HeadingPath { get; set; }

        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(IEnumerable), typeof(MapItemsSourceBehavior), new PropertyMetadata(OnPropertyChanged));

        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public static readonly DependencyProperty SymbolProperty =
            DependencyProperty.Register("Symbol", typeof(Symbol), typeof(MapItemsSourceBehavior), new PropertyMetadata(OnPropertyChanged));

        public Symbol Symbol
        {
            get { return (Symbol)GetValue(SymbolProperty); }
            set { SetValue(SymbolProperty, value); }
        }

        protected override void OnAttached()
        {
            AssociatedObject.Loaded += AssociatedObject_Loaded;

        }

        void layer_MouseLeftButtonDown(object sender, GraphicMouseButtonEventArgs e)
        {
            Graphic graphic = e.Graphic;
           
            var item = graphic.Attributes.Values.FirstOrDefault() as Entity;

            if (item != null)
            {
                var vehicles = ServiceLocator.Current.GetInstance<IVehiclesSource>().Vehicles;
              
                item.IsSelected = !item.IsSelected;
                foreach (GuiQuadVehicle vehicle in vehicles.Cast<GuiQuadVehicle>().Where(vehicle => vehicle.ID == item.Id))
                {
                    vehicle.IsSelected = item.IsSelected;
                }

            }
        }

        void AssociatedObject_MouseClick(object sender, Map.MouseEventArgs e)
        {

        }

        void AssociatedObject_TouchDown(object sender, System.Windows.Input.TouchEventArgs e)
        {
            throw new NotImplementedException();
        }

        void AssociatedObject_Loaded(object sender, RoutedEventArgs e)
        {
            _applied = true;
            SetUpMap();

        }

        protected override void OnDetaching()
        {
            AssociatedObject.Loaded -= AssociatedObject_Loaded;

            _applied = false;
            Remove();
        }

        private static void OnPropertyChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            MapItemsSourceBehavior me = (MapItemsSourceBehavior)sender;
            if (me._applied)
            {
                me.SetUpMap();
            }
        }

        private void SetUpMap()
        {
            GraphicsLayer layer =
                AssociatedObject.Layers.FirstOrDefault(l => !String.IsNullOrWhiteSpace(l.ID) && l.ID.Equals(ID)) as GraphicsLayer;

            if (ItemsSource != null && Symbol != null && !String.IsNullOrWhiteSpace(ID))
            {
                if (layer != null) AssociatedObject.Layers.Remove(layer);
                layer = new GraphicsLayer();
                layer.MouseLeftButtonDown += layer_MouseLeftButtonDown;
                layer.Renderer = new SimpleRenderer() { Symbol = Symbol };

                PointDataSource pds = new PointDataSource();
                pds.XCoordinateBinding = new Binding(XPropertyPath);
                pds.YCoordinateBinding = new Binding(YPropertyPath);
                pds.IsSelectedBinding = new Binding(SelectedPropertyPath);
                pds.ItemsSource = ItemsSource;
                pds.DataSpatialReference = new SpatialReference(Esri.GCS_WGS_1984);
                layer.GraphicsSource = pds;
                layer.ID = ID;
                AssociatedObject.Layers.Add(layer);
            }
            else if (layer != null) AssociatedObject.Layers.Remove(layer);
        }
        private void Remove()
        {
            var layer = AssociatedObject.Layers.FirstOrDefault(l => l.ID.Equals(ID));
            if (layer != null) AssociatedObject.Layers.Remove(layer);
        }
    }
}
