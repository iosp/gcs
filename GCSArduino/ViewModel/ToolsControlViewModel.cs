using System.ComponentModel;
using System.Runtime.CompilerServices;
using Common.Annotations;
using ESRI.ArcGIS.Client;
using GCSArduino.Interface;
using GalaSoft.MvvmLight.Command;
using Microsoft.Practices.ServiceLocation;

namespace GCSArduino.ViewModel
{
    public class ToolsControlViewModel : INotifyPropertyChanged, IToolsControl
    {
        public RelayCommand<object> CommandLos { get; set; }
        public RelayCommand<object> CommandPaint { get; set; }
        public RelayCommand<object> CommandZoom { get; set; }
        public RelayCommand<object> CommandZoomValueChanged { get; set; }

        public RelayCommand<object> CommandHand { get; set; }
        public RelayCommand<object> CommandRuler { get; set; }
        public RelayCommand<object> CommandRasterOnMap { get; set; }

        public RelayCommand<object> CommandZoomOut { get; set; }
        public RelayCommand<object> CommandZoomIn { get; set; }

        public ToolsControlViewModel()
        {

            CommandLos = new RelayCommand<object>(CommandLosClick);

            CommandPaint = new RelayCommand<object>(CommandPaintClick);

            CommandZoom = new RelayCommand<object>(CommandZoomClick);

            CommandHand = new RelayCommand<object>(CommandHandClick);

            CommandRuler = new RelayCommand<object>(CommandRulerClick);

            CommandRasterOnMap = new RelayCommand<object>(CommandRasterOnMapclick);

            CommandZoomOut = new RelayCommand<object>(CommandZoomOutClick);

            CommandZoomIn = new RelayCommand<object>(CommandZoomInClick);
        }

        private void CommandZoomInClick(object obj)
        {
            ZoomIn();
            ZoomInIsChecked = false;
        }

        private void CommandZoomOutClick(object obj)
        {
            ZoomOut();
            ZoomOutIsChecked = false;
        }

        #region Property

        private int _brightnessValue;
        public int BrightnessValue
        {
            get { return _brightnessValue; }
            set { _brightnessValue = value;  OnPropertyChanged(); }
        }

        private int _zoomSlider;
        public int ZoomSlider
        {
            get { return _zoomSlider; }
            set
            {
                _zoomSlider = value;
                OnPropertyChanged();
            }
        }

        private bool _losIsChecked;
        public bool LosIsChecked
        {
            get { return _losIsChecked; }
            set
            {
                _losIsChecked = value;
                OnPropertyChanged();
            }
        }

        private bool _ormapIsChecked;
        public bool OrmapIsChecked
        {
            get { return _ormapIsChecked; }
            set { _ormapIsChecked = value; OnPropertyChanged(); }
        }

        private bool _rasterIsChecked;
        public bool RasterIsChecked
        {
            get { return _rasterIsChecked; }
            set { _rasterIsChecked = value; OnPropertyChanged(); }
        }

        private bool _zoomAllIsChecked;
        public bool ZoomAllIsChecked
        {
            get { return _zoomAllIsChecked; }
            set { _zoomAllIsChecked = value; OnPropertyChanged(); }
        }
        private bool _zoomOutIsChecked;
        public bool ZoomOutIsChecked
        {
            get { return _zoomOutIsChecked; }
            set { _zoomOutIsChecked = value; OnPropertyChanged(); }
        }
        private bool _zoomInIsChecked;
        public bool ZoomInIsChecked
        {
            get { return _zoomInIsChecked; }
            set { _zoomInIsChecked = value; OnPropertyChanged(); }
        }

        private bool _rectIsChecked;
        public bool RectIsChecked
        {
            get { return _rectIsChecked; }
            set { _rectIsChecked = value; OnPropertyChanged(); }
        }
        private bool _circleIsChecked;
        public bool CircleIsChecked
        {
            get { return _circleIsChecked; }
            set { _circleIsChecked = value; OnPropertyChanged(); }
        }
        private bool _areaIsChecked;
        public bool AreaIsChecked
        {
            get { return _areaIsChecked; }
            set { _areaIsChecked = value; OnPropertyChanged(); }
        }
        private bool _polylineIsChecked;
        public bool PolylineIsChecked
        {
            get { return _polylineIsChecked; }
            set { _polylineIsChecked = value; OnPropertyChanged(); }
        }
        private bool _pointIsChecked;
        public bool PointIsChecked
        {
            get { return _pointIsChecked; }
            set { _pointIsChecked = value; OnPropertyChanged(); }
        }

        private bool _handIsChecked;
        public bool HandIsChecked
        {
            get { return _handIsChecked; }
            set { _handIsChecked = value; OnPropertyChanged(); }
        }

        private bool _rulerIsChecked;
        public bool RulerIsChecked
        {
            get { return _rulerIsChecked; }
            set { _rulerIsChecked = value; OnPropertyChanged(); }
        }
        #endregion

        #region Commands Function

        private void CommandZoomClick(object obj)
        {
            var state = obj as string;
            switch (state)
            {
                case "ZoomIn":
                    ZoomIn();
                    break;

                case "ZoomOut":
                    ZoomOut();
                    break;

                case "ZoomAll":
                    ZoomMode();
                    break;

            }
        }

        private void CommandPaintClick(object obj)
        {
            var state = obj as string;
            ServiceLocator.Current.GetInstance<IMap>().DrawMode = DrawMode.None;
            switch (state)
            {
                case "Rect":
                    
                    ServiceLocator.Current.GetInstance<IMap>().DrawMode = DrawMode.Rectangle;
                    break;
                case "Circle":
                    ServiceLocator.Current.GetInstance<IMap>().DrawMode = DrawMode.Circle;
                    break;
                case "Area":
                    ServiceLocator.Current.GetInstance<IMap>().DrawMode = DrawMode.Polygon;
                    break;
                case "Polyline":
                    ServiceLocator.Current.GetInstance<IMap>().DrawMode = DrawMode.Polyline;
                    break;
                case "Point":
                    ServiceLocator.Current.GetInstance<IMap>().DrawMode = DrawMode.Point;
                    break;

            }
        }



        private void CommandHandClick(object obj)
        {
            HandIsChecked = false;
        }

        private void CommandRulerClick(object obj)
        {
            RulerIsChecked = false;
        }

        private void CommandRasterOnMapclick(object obj)
        {
        }

        private void CommandLosClick(object obj)
        {
        }

        #endregion

        #region Controls

        #region Zoom

        public void ZoomIn()
        {
        }

        public void ZoomOut()
        {
        }


        public void ZoomMode()
        {
        }
        #endregion

        #region OrientationMap
        public void OrientationMap(int? data)
        {
        }

        #endregion

        #region Mode
        public void HandMode(int? data)
        {
        }

        public void RulerMode()
        {
        }


        #endregion

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
