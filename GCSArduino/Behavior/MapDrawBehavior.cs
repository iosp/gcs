using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
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
    public class MapDrawBehavior : Behavior<Map>
    {
        public static readonly DependencyProperty ItemsSourceProperty =
         DependencyProperty.Register("ItemsSource", typeof(ICollection<Shape>), typeof(MapDrawBehavior));

        public ICollection<Shape> ItemsSource
        {
            get { return (ICollection<Shape>)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public static readonly DependencyProperty DrawModeProperty = DependencyProperty.Register(
            "DrawMode", typeof(DrawMode), typeof(MapDrawBehavior),
            new PropertyMetadata(default(DrawMode), OnDrawModeChanged));

        public DrawMode DrawMode
        {
            get { return (DrawMode)GetValue(DrawModeProperty); }
            set { SetValue(DrawModeProperty, value); }
        }

        public static readonly DependencyProperty NewTaskShapeIdProperty = DependencyProperty.Register(
            "NewTaskShapeId", typeof(ushort), typeof(MapDrawBehavior), new PropertyMetadata(default(ushort)));

        public ushort NewTaskShapeId
        {
            get { return (ushort)GetValue(NewTaskShapeIdProperty); }
            set { SetValue(NewTaskShapeIdProperty, value); }
        }

        public static readonly DependencyProperty SelectedShapeProperty = DependencyProperty.Register(
            "SelectedShape", typeof(Shape), typeof(MapDrawBehavior), new PropertyMetadata(default(Shape)));

        public Shape SelectedShape
        {
            get { return (Shape)GetValue(SelectedShapeProperty); }
            set { SetValue(SelectedShapeProperty, value); }
        }
        public static readonly DependencyProperty LineSymbolProperty =
            DependencyProperty.Register("LineSymbol", typeof(LineSymbol), typeof(MapDrawBehavior));

        public LineSymbol LineSymbol
        {
            get { return (LineSymbol)GetValue(LineSymbolProperty); }
            set { SetValue(LineSymbolProperty, value); }
        }

        public static readonly DependencyProperty FillSymbolProperty =
            DependencyProperty.Register("FillSymbol", typeof(FillSymbol), typeof(MapDrawBehavior));

        public FillSymbol FillSymbol
        {
            get { return (FillSymbol)GetValue(FillSymbolProperty); }
            set { SetValue(FillSymbolProperty, value); }
        }

        private static void OnDrawModeChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            ((MapDrawBehavior)dependencyObject).OnDrawModeChanged();
        }

        private Draw _drawingObject;


        private void CreateDrawingObject()
        {
            _drawingObject = new Draw(AssociatedObject)
            {

                LineSymbol = new SimpleLineSymbol(Colors.Blue, 6.0) { Style = SimpleLineSymbol.LineStyle.Solid },
                FillSymbol = new SimpleFillSymbol
                {
                    Fill = new SolidColorBrush(Colors.Red) { Opacity = 0.5 },
                    BorderBrush = new SolidColorBrush(Colors.Red),
                    BorderThickness = 4.0
                }

            };

            _drawingObject.DrawBegin += DrawingObject_DrawBegin;
            _drawingObject.DrawComplete += WhenDrawComplete;

        }

        void DrawingObject_DrawBegin(object sender, System.EventArgs e)
        {
            Mouse.Capture(AssociatedObject);
        }

        private void WhenDrawComplete(object sender, DrawEventArgs e)
        {
            OnComplete(e.Geometry);
            Mouse.Capture(AssociatedObject, CaptureMode.None);
            DrawMode = DrawMode.None;
        }

        private void OnDrawModeChanged()
        {
            if (_drawingObject != null)
            {
                _drawingObject.DrawMode = DrawMode;
                _drawingObject.IsEnabled = DrawMode != DrawMode.None;
            }
            //LineDrawMode = DrawMode != DrawMode.None;
        }

      
        private void OnComplete(ESRI.ArcGIS.Client.Geometry.Geometry geometry)
        {
            var shapeId = ServiceLocator.Current.GetInstance<IMissionSource>().GenerateTaskId();
            if (ItemsSource != null && DrawMode != DrawMode.None)
            {
                geometry.SpatialReference = new SpatialReference(Esri.GCS_WGS_1984);
                ItemsSource.Add(new Shape
                    {
                        Geometry = geometry,
                        ID = shapeId

                    });
                var geo = geometry as Polyline;
                if (geo != null)
                {
                    var points = geo.Paths.ToList()[0];
                    var pointList = points.Select(point => new Point(point.X, point.Y)).ToList();

                    Messenger.Default.Send(new MapWaypointMessenger
                    {
                        Points = pointList,
                        TaskId = shapeId
                        
                    });
                }
            }
        }

        protected override void OnAttached()
        {
            CreateDrawingObject();
        }


    }
}

