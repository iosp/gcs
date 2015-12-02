using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using ESRI.ArcGIS.Client;
using ESRI.ArcGIS.Client.Geometry;

using System.Windows.Interactivity;
using GCSArduino.Messengers;
using GalaSoft.MvvmLight.Messaging;


namespace GCSArduino.Behavior
{
    public class MapTileSourceBehavior : Behavior<Map>
    {
        public string ID { get; set; }

        public static readonly DependencyProperty CurrentExtentProperty = DependencyProperty.RegisterAttached(
           "CurrentExtent", typeof(Envelope), typeof(MapTileSourceBehavior), new PropertyMetadata(null));

        public static Envelope GetCurrentExtent(DependencyObject obj)
        {
            return (Envelope)obj.GetValue(CurrentExtentProperty);
        }

        public static void SetCurrentExtent(DependencyObject obj, Envelope value)
        {
            obj.SetValue(CurrentExtentProperty, value);
        }

        public static readonly DependencyProperty PathProperty =
            DependencyProperty.Register("Path", typeof(string), typeof(MapTileSourceBehavior), new PropertyMetadata(default(string)));

        public string Path
        {
            get { return (string) GetValue(PathProperty); }
            set { SetValue(PathProperty, value); }
        }

        public static readonly DependencyProperty FromEsriServerProperty =
            DependencyProperty.Register("FromEsriServer", typeof (bool), typeof (MapTileSourceBehavior), new PropertyMetadata(default(bool)));

        public bool FromEsriServer
        {
            get { return (bool) GetValue(FromEsriServerProperty); }
            set { SetValue(FromEsriServerProperty, value); }
        }

        public static readonly DependencyProperty OpacityProperty = DependencyProperty.Register(
           "Opacity", typeof(double), typeof(MapTileSourceBehavior), new PropertyMetadata(OpacityPropertyChanged));

        private static void OpacityPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var mc = d as MapTileSourceBehavior;
            if (mc != null)
            {
                if (mc.AssociatedObject != null && mc.AssociatedObject.Layers.Count > 0)
                {
                    var staticLayer = mc.AssociatedObject.Layers[0];
                    staticLayer.Opacity = (double)e.NewValue;
                    //staticLayer.SetValue();
                }
            }
        }

        public double Opacity
        {
            get { return (double)GetValue(OpacityProperty); }
            set { SetValue(OpacityProperty, value); }
        }
        public static readonly DependencyProperty IsLoadingProperty =
          DependencyProperty.Register("IsLoading", typeof(bool), typeof(MapTileSourceBehavior), new PropertyMetadata(false));

        public bool IsLoading
        {
            get { return (bool)GetValue(IsLoadingProperty); }
            set { SetValue(IsLoadingProperty, value); }
        }

        public static readonly DependencyProperty ProgressProperty =
            DependencyProperty.Register("Progress", typeof(int), typeof(MapTileSourceBehavior));

        public int Progress
        {
            get { return (int)GetValue(ProgressProperty); }
            set { SetValue(ProgressProperty, value); }
        }


        protected override void OnAttached()
        {
            var staticLayer = CreateStaticLayer();
            
            if(staticLayer == null) return;

            IsLoading = true;
            Progress = 0;
            AssociatedObject.Progress += (o, e) => Progress = e.Progress;
            staticLayer.Opacity = Opacity;
            AssociatedObject.MouseClick += AssociatedObject_MouseClick;
            
            AssociatedObject.Layers.Insert(0, staticLayer);
            
        }

        private void AssociatedObject_MouseClick(object sender, Map.MouseEventArgs e)
        {
            Messenger.Default.Send(new MapMouseLeftButtonDownMessenger{Latitude = e.MapPoint.Y,Longitude =e.MapPoint.X,Altitude = (int) e.MapPoint.Z});
        }


        void AssociatedObject_MouseRightButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Messenger.Default.Send(new MapMouseRightButtonDownMessenger());
        }

       

        protected override void OnDetaching()
        {
            
        }
        private Layer CreateStaticLayer()
        {
            Layer layer = null;
            if (!string.IsNullOrWhiteSpace(Path) && File.Exists(Path))
            {
                layer =
                    AssociatedObject.Layers.FirstOrDefault(
                        l => !String.IsNullOrWhiteSpace(l.ID) && l.ID.Equals(ID)) as ArcGISLocalTiledLayer;

                if (layer != null) return null; // no need to add it is already added

                layer = new ArcGISLocalTiledLayer(Path);
                //layer = new ArcGISTiledMapServiceLayer() { Url = Path };
                
                OnAddLayer();

                layer.ID = ID;

                layer.Initialized += OnLayerInitalized;
                layer.InitializationFailed += (o, e) =>
                {
                    OnDone();
                };


            }
            else if (!string.IsNullOrWhiteSpace(Path))
            {
                layer =
                    AssociatedObject.Layers.FirstOrDefault(
                        l => !String.IsNullOrWhiteSpace(l.ID) && l.ID.Equals(ID)) as ArcGISLocalTiledLayer;

                if (layer != null) return null; // no need to add it is already added

                //layer = new ArcGISLocalTiledLayer(Path);
                layer = new ArcGISTiledMapServiceLayer() { Url = Path };

                OnAddLayer();

                layer.ID = ID;

                layer.Initialized += OnLayerInitalized;
                layer.InitializationFailed += (o, e) =>
                {
                    OnDone();
                };
            }
            return layer;

        }
        private void OnLayerInitalized(object o, EventArgs e)
        {
            OnDone(o as TiledLayer);
        }
        private volatile int _loadCount = 0;

        private void OnAddLayer()
        {
            Interlocked.Increment(ref _loadCount);
        }
        private void OnDone(TiledLayer layer = null)
        {
            var curCount = Interlocked.Decrement(ref _loadCount);
            if (layer != null)
                AssociatedObject.SetCurrentValue(CurrentExtentProperty, layer.FullExtent);
            if (curCount != 0) return;


            IsLoading = false;
            Progress = 0;
        }
        //private Layer CreateArcGISServiceLayer()
        //{
        //    Layer layer = null;

        //    if (!string.IsNullOrWhiteSpace(MapPath))
        //    {
        //        layer =
        //            AssociatedObject.Map.Layers.FirstOrDefault(
        //                tempLayer =>
        //                !string.IsNullOrWhiteSpace(tempLayer.ID) && tempLayer.ID.Equals(ID)) as ArcGISLocalTiledLayer;
        //        if (layer != null) return null;

        //        layer = new ArcGISTiledMapServiceLayer(new Uri(MapPath));
        //        layer.ID = ID;

        //    }
        //    return layer; 
        //}
    }
}
