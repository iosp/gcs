using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using ESRI.ArcGIS.Client;
using ESRI.ArcGIS.Client.Geometry;
using ESRI.ArcGIS.Client.Symbols;
using GCSArduino.Model;


namespace GCSArduino.Behavior
{
    public class GeometryGraphicsDataSource : GraphicsDataSource
    {
        private class DataGeometry : GraphicsDataSource.DataObject
        {
            public static readonly DependencyProperty ShapeGeometryProperty =
                DependencyProperty.Register("ShapeGeometry", typeof(Geometry), typeof(DataGeometry),
                new PropertyMetadata(OnShapeGeometryChanged));

            public Geometry ShapeGeometry
            {
                get
                {
                    return (Geometry)base.GetValue(ShapeGeometryProperty);
                }
                set
                {
                    base.SetValue(ShapeGeometryProperty, value);
                }
            }

            public static readonly DependencyProperty SymbolProperty = DependencyProperty.Register(
                "Symbol", typeof(Symbol), typeof(DataGeometry), new PropertyMetadata(OnSymbolChanged));

            private static void OnSymbolChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                DataGeometry data = (DataGeometry)d;
                Symbol symbol = (Symbol)e.NewValue;
                data.Symbol = symbol;
            }

            public Symbol Symbol
            {
                get { return (Symbol)GetValue(SymbolProperty); }
                set { SetValue(SymbolProperty, value); }
            }

            private static void OnShapeGeometryChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                DataGeometry data = (DataGeometry)d;
                Geometry geometry = (Geometry)e.NewValue;
                data.Geometry = geometry;

            }
        }
        private Binding _geometryBinding;
        private Binding _symbolBinding;

        public Binding GeometryBinding
        {
            get
            {
                return _geometryBinding;
            }
            set
            {
                if (value != _geometryBinding)
                {
                    _geometryBinding = value;
                    Refresh();
                }
            }
        }

        public Binding SymbolBinding
        {
            get { return _symbolBinding; }
            set
            {
                _symbolBinding = value;
                Refresh();
            }
        }

        protected override GraphicsDataSource.DataObject CreateDataPoint(object dataContext)
        {
            DataGeometry data = new DataGeometry();
            data.Graphic.Attributes["DataContext"] = dataContext;
            var Dynamic = dataContext as Shape;
            data.Graphic.Symbol = Dynamic.Symbol;
            return data;
        }

        protected override void PrepareDataPoint(GraphicsDataSource.DataObject dataPoint, object dataContext)
        {
            base.PrepareDataPoint(dataPoint, dataContext);
            if (GeometryBinding != null)
            {
                dataPoint.SetBinding(DataGeometry.ShapeGeometryProperty, GeometryBinding);
                // dataPoint.SetBinding(DataGeometry.SymbolProperty, SymbolBinding);
            }
        }
    }

}
