using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ESRI.ArcGIS.Client;
using ESRI.ArcGIS.Client.Symbols;

namespace GCSArduino.Behavior
{
    public class MyRenderer : IRenderer
    {
        public MyRenderer()
        {
            Symbol1 = (SimpleFillSymbol)Application.Current.FindResource("PolygonSymbol");//new SimpleFillSymbol();
            Symbol2 = (LineSymbol)Application.Current.FindResource("LineSymbol");//new SimpleFillSymbol();
            Symbol3 = (SimpleMarkerSymbol)Application.Current.FindResource("PointSymbol");//new SimpleFillSymbol();

        }
        public SimpleFillSymbol Symbol1 { get; set; }

        public LineSymbol Symbol2 { get; set; }

        public SimpleMarkerSymbol Symbol3 { get; set; }

        public Symbol GetSymbol(Graphic graphic)
        {
            if (graphic.Symbol != null)
                return graphic.Symbol;
            var g = graphic.Geometry as ESRI.ArcGIS.Client.Geometry.Polyline;
            if (g != null)
                return Symbol2;

            var g1 = graphic.Geometry as ESRI.ArcGIS.Client.Geometry.Polygon;
            if (g1 != null)
                return Symbol1;

            var g3 = graphic.Geometry as ESRI.ArcGIS.Client.Geometry.MapPoint;
            if (g3 != null)
                return Symbol3;

            var g4 = graphic.Geometry as ESRI.ArcGIS.Client.Geometry.MultiPoint;
            if (g4 != null)
                return Symbol3;

            return null;
        }
    }
}