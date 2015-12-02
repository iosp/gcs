using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Interactivity;
using System.Windows.Media;

using ESRI.ArcGIS.Client;
using ESRI.ArcGIS.Client.Geometry;
using ESRI.ArcGIS.Client.Symbols;
using GCSArduino.Interface;
using GCSArduino.Messengers;
using GCSArduino.Model;
using GCSArduino.Utils;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Practices.ServiceLocation;
using MissionPackage;

namespace GCSArduino.Behavior
{
    public class MapGraphicItemsSourceBehavior : Behavior<Map>
    {
        private bool _applied;
        private const int SatrtWayPointId = 1;
        private const int ConnectionLineId = 0;
        private const double DefaultWayPointOffsetX = 35;
        private const double DefaultWayPointOffsetY = 35;
        private const double DefaultWayPointFontSize = 20;
        private readonly FontWeight DefaultWayPointFontWeight = FontWeights.Bold;
        private readonly Brush DefaultWayPointForeground = Brushes.DeepPink;
        private const string DefaultWayPointPrefix = "WP ";

        private List<Shape> _vertexGraphicList = new List<Shape>();

        public string ID { get; set; }
        public string GeometryPropertyPath { get; set; }
        public string SymbolPropertyPath { get; set; }

        public EditGeometry EditGeometry;
        public Shape EditShape { get; set; }

        public static readonly DependencyProperty ItemsSourceProperty =
         DependencyProperty.Register("ItemsSource", typeof(ICollection<Shape>), typeof(MapGraphicItemsSourceBehavior));

        public ICollection<Shape> ItemsSource
        {
            get { return (ICollection<Shape>)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public static readonly DependencyProperty SymbolProperty =
            DependencyProperty.Register("Symbol", typeof(Symbol), typeof(MapGraphicItemsSourceBehavior), new PropertyMetadata(OnPropertyChanged));

        public Symbol Symbol
        {
            get { return (Symbol)GetValue(SymbolProperty); }
            set { SetValue(SymbolProperty, value); }
        }
       
        public static readonly DependencyProperty ShapeSelectedProperty = DependencyProperty.Register(
            "ShapeSelected", typeof(Shape), typeof(MapGraphicItemsSourceBehavior), new PropertyMetadata(OnDynamicShapeChanged));

        private static void OnDynamicShapeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Shape dynamicShape = e.NewValue as Shape;
            MapGraphicItemsSourceBehavior mapGraphicItemsSourceBehavior = (MapGraphicItemsSourceBehavior)d;
            if (mapGraphicItemsSourceBehavior != null && mapGraphicItemsSourceBehavior.AssociatedObject != null)
            {
                Graphic graphic = null;
                var graphicsLayer = mapGraphicItemsSourceBehavior.AssociatedObject.Layers["Shapes"] as GraphicsLayer;
                if (dynamicShape != null)
                    if (graphicsLayer != null)
                    {
                        graphic = graphicsLayer.Graphics.FirstOrDefault(p => p.Geometry == dynamicShape.Geometry);
                        if (graphic != null)
                        {
                            bool isValid = mapGraphicItemsSourceBehavior.IsValidGraphicChecked(graphic);
                            if (isValid)
                                mapGraphicItemsSourceBehavior.SelectByGraphic(graphic);
                        }
                    }
            }
        }

        public Shape ShapeSelected
        {
            get { return (Shape)GetValue(ShapeSelectedProperty); }
            set
            {
                SetValue(ShapeSelectedProperty, value);
            }
        }

        public static readonly DependencyProperty EditGeometryActionProperty = DependencyProperty.Register(
            "EditGeometryAction", typeof(EditGeometry.Action?), typeof(MapGraphicItemsSourceBehavior), new PropertyMetadata(OnEditGeometryActionChanged));

        private static void OnEditGeometryActionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MapGraphicItemsSourceBehavior me = (MapGraphicItemsSourceBehavior)d;
            if (me != null)
            {
                if (me.EditGeometryAction == EditGeometry.Action.EditCanceled)
                {
                    if(me.ItemsSource != null)
                        me.ItemsSource.Remove(me.EditShape);
                    me.EditShape = null;
                    me.EditGeometry.IsEnabled = false;
                    me.ClearSelection();
                    me.EditGeometry.StopEdit();
                }
                else if (me.EditGeometryAction == EditGeometry.Action.EditCompleted)
                {
                    me.EditGeometry.StopEdit();
                }
            }
        }

        public EditGeometry.Action? EditGeometryAction
        {
            get { return (EditGeometry.Action?)GetValue(EditGeometryActionProperty); }
            set { SetValue(EditGeometryActionProperty, value); }
        }

        protected override void OnAttached()
        {
            AssociatedObject.Loaded += AssociatedObject_Loaded;
            AssociatedObject.MouseMove += AssociatedObject_MouseMove;
        }

        void AssociatedObject_MouseMove(object sender, MouseEventArgs e)
        {
            if (EditShape != null)
            {
                if (Mouse.LeftButton == MouseButtonState.Pressed && _vertexGraphicList.Count > 0)
                {
                    _vertexGraphicList.ForEach(f => ItemsSource.Remove(f));
                    _vertexGraphicList.Clear();
                }
            }
        }

        void AssociatedObject_Loaded(object sender, RoutedEventArgs e)
        {
            EditGeometry = new EditGeometry();
            EditGeometry.IsEnabled = true;
            EditGeometry.VertexSymbol = (MarkerSymbol)Application.Current.FindResource("EditVertexMarkerSymbol");
            EditGeometry.ScalePointSymbol = (SimpleMarkerSymbol)Application.Current.FindResource("MyScaleSymbol");
            EditGeometry.ScaleBoxSymbol = (SimpleLineSymbol)Application.Current.FindResource("MyScaleBox");
            EditGeometry.GeometryEdit += EditGeometry_GeometryEdit;
            EditGeometry.Map = AssociatedObject;
            _applied = true;
           
            SetUpMap();
            GraphicsLayer graphicsLayer = AssociatedObject.Layers["Shapes"] as GraphicsLayer;
            graphicsLayer.MouseLeftButtonDown += graphicsLayer_MouseLeftButtonDown;
            graphicsLayer.MouseLeftButtonUp += graphicsLayer_MouseLeftButtonUp;
            AssociatedObject.TouchDown += AssociatedObject_TouchDown;
            AssociatedObject.MapGesture += AssociatedObject_MapGesture;
            AssociatedObject.MouseClick += AssociatedObject_MouseClick;
        }

        void graphicsLayer_MouseLeftButtonDown(object sender, GraphicMouseButtonEventArgs e)
        {
            e.Handled = true;
            Graphic graphic = e.Graphic;
            Shape dynamicShape = ItemsSource.FirstOrDefault(i => i.Geometry == graphic.Geometry);
            if (dynamicShape.ID == Const.ConstConnectionLine || dynamicShape.ID == Const.ConstWayPoint)
                return;
            var graphicsLayer = AssociatedObject.Layers["Shapes"] as GraphicsLayer;
            _vertexGraphicList.ForEach(f => ItemsSource.Remove(f));
            _vertexGraphicList.Clear();
             Graphic prevGraphic = null;
            if(EditShape != null)
                prevGraphic = graphicsLayer.Graphics.FirstOrDefault(p => p.Geometry == EditShape.Geometry);
            if (prevGraphic != null)
                prevGraphic.Selected = false;
        }

        void graphicsLayer_MouseLeftButtonUp(object sender, GraphicMouseButtonEventArgs e)
        {
            e.Handled = true;
            Graphic graphic = e.Graphic;
            Shape dynamicShape = ItemsSource.FirstOrDefault(i => i.Geometry == graphic.Geometry);
            if (dynamicShape.ID == Const.ConstConnectionLine || dynamicShape.ID == Const.ConstWayPoint)
                return;
            var graphicsLayer = AssociatedObject.Layers["Shapes"] as GraphicsLayer;
            bool isValid = IsValidGraphicChecked(graphic);
            if (!isValid)
                return;
           
            SelectByGraphic(graphic);

            if (dynamicShape != null)
                Messenger.Default.Send(new TaskSelectedByShapeMessenger
                {
                    SelectedTaskId = dynamicShape.ID
                });
        }

        void AssociatedObject_MouseClick(object sender, Map.MouseEventArgs e)
        {
            IsValidGraphicChecked(null);
        }

        void AssociatedObject_MapGesture(object sender, Map.MapGestureEventArgs e)
        {

        }

        void AssociatedObject_TouchDown(object sender, TouchEventArgs e)
        {
            //var point = e.GetTouchPoint(AssociatedObject);

            //var graphicsLayer = AssociatedObject.Layers["Shapes"] as GraphicsLayer;
            //if (graphicsLayer != null)
            //{
            //    var graphics = graphicsLayer.FindGraphicsInHostCoordinates(point.Position);
            //    var entity = graphics.First();
            //    entity.Selected = true;
            //}
        }

     
        private bool IsValidGraphicChecked(Graphic graphic)
        {
            var graphicsLayer = AssociatedObject.Layers["Shapes"] as GraphicsLayer;
            if (graphicsLayer != null)
            {
                if (graphic == null || (EditShape != null && EditShape.Geometry == graphic.Geometry))
                {
                    EditGeometry.CancelEdit();
                    EditGeometry.IsEnabled = false;
                    if (graphic != null)
                        graphic.Selected = false;
                    _vertexGraphicList.ForEach(f => ItemsSource.Remove(f));
                    _vertexGraphicList.Clear();
                    graphicsLayer.ClearSelection();
                    return false;                   
                }
            }
            return true;
        }
        private bool _isSelected = false;
        private void SelectByGraphic(Graphic graphic)
        {
            //Check recursive call
            if (_isSelected)
                return;
            try
            {
                _isSelected = true;
                ClearSelection();
                EditGeometry.CancelEdit();
                EditGeometryAction = EditGeometry.Action.EditStarted;
                graphic.Selected = true;

                EditGeometry.IsEnabled = true;
                EditGeometry.StartEdit(graphic);
                if (ItemsSource == null) return;
                {
                    
                }
                foreach (var dynamicShape in ItemsSource)
                {
                    if (dynamicShape.Geometry == graphic.Geometry)
                    {
                        ShapeSelected = dynamicShape;
                        break;
                    }
                }

                EditShape = new Shape(Const.ConstCloneShape);
                var temp = ShapeSelected.Geometry as Polyline;
                if (temp != null)
                {
                    EditShape.Geometry = ESRI.ArcGIS.Client.Geometry.Geometry.Clone(temp);
                    ItemsSource.Add(EditShape);
                }
                _isSelected = false;
            }
            catch
            {
                _isSelected = false;
            }
        }

        private void ClearSelection()
        {
            var graphicsLayer = AssociatedObject.Layers["Shapes"] as GraphicsLayer;
            if (graphicsLayer != null)
            {
                Graphic graphic = graphicsLayer.Graphics.FirstOrDefault(f => f.Selected);
                if (graphic != null)
                {
                    graphic.Selected = false;
                }
            }
        }

        void EditGeometry_GeometryEdit(object sender, EditGeometry.GeometryEditEventArgs e)
        {
            if (e.Graphic != null && (e.Action == EditGeometry.Action.EditCompleted))
            {
                if (e.Graphic.Selected)
                {
                    e.Graphic.Selected = false;

                    if (ShapeSelected != null)
                        ShapeSelected.Geometry = e.Geometry;

                    ItemsSource.Remove(EditShape);
                    EditShape = null;
                    EditGeometry.IsEnabled = false;
                    EditGeometry.StopEdit();

                    //var geo = e.Geometry as Polyline;
                    //if (geo != null)
                    //{
                    //    var points = geo.Paths.ToList()[0];
                    //    var pointList = new List<Point>();

                    //    foreach (var point in points)
                    //    {
                    //        pointList.Add(new Point(point.X, point.Y));
                    //    }

                    //    Messenger.Default.Send(new MapWaypointMessenger
                    //    {
                    //        Points = pointList,
                    //        TaskId = 0,
                    //    });
                    //}
                    var mapControl = ServiceLocator.Current.GetInstance<IMap>();
                    if (mapControl != null)
                    {
                        var pointList = new List<Point>();
                        ushort taskId = 0;
                        foreach (var guiTask in mapControl.ShapesItemsSource)
                        {
                            var guiTaskNav = guiTask as Shape;
                            var polyline = e.Geometry as Polyline;
                            if (guiTaskNav != null)
                            {
                                if (guiTaskNav.Geometry == e.Geometry)
                                {

                                    foreach (var path in polyline.Paths.ToList())
                                    {
                                        foreach (var p in path)
                                        {
                                            pointList.Add(new Point(p.X, p.Y));
                                        }
                                    }
                                    taskId = (ushort) guiTaskNav.ID;
                                }
                            }

                        }
                        if (taskId > 0 && pointList.Count > 1)
                            Messenger.Default.Send(new MapWaypointMessenger
                            {
                                Points = pointList,
                                TaskId = taskId
                            });
                    }
                }
            }
            else if (e.Action == EditGeometry.Action.EditCanceled)
            {
                ItemsSource.Remove(EditShape);
                EditShape = null;
                ShapeSelected = null;
                EditGeometry.IsEnabled = false;
                EditGeometry.StopEdit();
            }
            else if (e.Action == EditGeometry.Action.EditStarted)
            {
                _vertexGraphicList.ForEach(f => ItemsSource.Remove(f));
                _vertexGraphicList.Clear();
                AddVertexLabel(e.Geometry);
            }
            else if (e.Action == EditGeometry.Action.GeometryMoved ||
                   e.Action == EditGeometry.Action.GeometryScaled ||
                   e.Action == EditGeometry.Action.GeometryRotated ||
                   e.Action == EditGeometry.Action.VertexAdded ||
                   e.Action == EditGeometry.Action.VertexMoved ||
                   e.Action == EditGeometry.Action.VertexRemoved)
            {
                _vertexGraphicList.ForEach(f => ItemsSource.Remove(f));
                _vertexGraphicList.Clear();
                AddVertexLabel(e.Geometry);
            }
        }
        
        private void AddVertexLabel(ESRI.ArcGIS.Client.Geometry.Geometry geometry)
        {
            int index = SatrtWayPointId;
            var polyline = geometry as Polyline;
            if (polyline != null)
            {
                Dictionary<MapPoint, int> wayPointIdMap = GetWayPointId(geometry);
                foreach (MapPoint item in polyline.Paths.FirstOrDefault())
                {
                    Shape dynamicShape = new Shape(Const.ConstWayPoint);
                    var textSymbol = new TextSymbol() { OffsetX = DefaultWayPointOffsetX, OffsetY = DefaultWayPointOffsetY, FontSize = DefaultWayPointFontSize, Foreground = DefaultWayPointForeground };
                    if (wayPointIdMap.ContainsKey(item))
                        index = wayPointIdMap[item];
                    else
                        index++;
                    textSymbol.Text = DefaultWayPointPrefix + index.ToString();
                    dynamicShape.Geometry = item;
                    dynamicShape.Symbol = textSymbol;
                    _vertexGraphicList.Add(dynamicShape);
                    ItemsSource.Add(dynamicShape);
                }
            }
        }

        private  Dictionary<MapPoint, int> GetWayPointId(ESRI.ArcGIS.Client.Geometry.Geometry geometry)
        {
            int id = SatrtWayPointId;            
            var polyline = geometry as Polyline;
            foreach (Shape dynamicShape in ItemsSource)
            {
                var wayPointIdMap = new Dictionary<MapPoint, int>();
                var foundPolyline = dynamicShape.Geometry as Polyline;
                if(dynamicShape.ID != Const.ConstConnectionLine &&
                     dynamicShape.ID != Const.ConstWayPoint && 
                     foundPolyline != null)
                {
                    if (dynamicShape.Geometry == ShapeSelected.Geometry)
                        foundPolyline = polyline;
                    foreach (MapPoint item in foundPolyline.Paths.FirstOrDefault())
                    {
                        wayPointIdMap.Add(item, id++);
                    }                    
                }
                if (dynamicShape.Geometry == ShapeSelected.Geometry)
                    return wayPointIdMap;
            }
            return null;
        }

        private TextSymbol GetTextSymbol(MapPoint point)
        {
            TextSymbol textSymbol = new TextSymbol()
            {
                FontFamily = new FontFamily("Arial"),
                Foreground = new SolidColorBrush(Colors.Black),
                FontSize = 20,
                Text = string.Format("X:{0} Y:{1}", point.X, point.Y)
            };
            return textSymbol;
        }
        protected override void OnDetaching()
        {
            AssociatedObject.Loaded -= AssociatedObject_Loaded;
            _applied = false;
            Remove();
        }

        private static void OnPropertyChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var me = (MapGraphicItemsSourceBehavior)sender;
            if (me._applied)
            {
                me.SetUpMap();
            }
        }

        private void SetUpMap()
        {
            var layer = AssociatedObject.Layers.FirstOrDefault(l => !String.IsNullOrWhiteSpace(l.ID) && l.ID.Equals(ID)) as GraphicsLayer;

            if (ItemsSource != null && Symbol != null && !String.IsNullOrWhiteSpace(ID))
            {
                if (layer != null)
                    AssociatedObject.Layers.Remove(layer);
                layer = new GraphicsLayer {Renderer = new MyRenderer()};

                var ggds = new GeometryGraphicsDataSource
                    {
                        GeometryBinding = new Binding(GeometryPropertyPath),
                        ItemsSource = ItemsSource
                    };

                layer.GraphicsSource = ggds;

                layer.MouseLeftButtonUp += WhenMouseLeftButtonUp;
                layer.MouseMove += layer_MouseMove;

                layer.ID = ID;
                AssociatedObject.Layers.Add(layer);
            }
            else if (layer != null) AssociatedObject.Layers.Remove(layer);
        }

        void layer_MouseMove(object sender, GraphicMouseEventArgs e)
        {

        }

        private void WhenMouseLeftButtonUp(object sender, GraphicMouseButtonEventArgs e)
        {


        }

        private void Remove()
        {
            var layer = AssociatedObject.Layers.FirstOrDefault(l => l.ID.Equals(ID));
            if (layer != null) AssociatedObject.Layers.Remove(layer);
        }
    }
}
