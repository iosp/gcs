using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESRI.ArcGIS.Client;
using GCSArduino.Model;

namespace GCSArduino.Interface
{
    public interface IMap
    {
        ObservableCollection<Shape> ShapesItemsSource { get; set; }
        ObservableCollection<Entity> EntitysItemsSource { get; set; }
        Entity SelectedEntity { get; set; }
        Shape SelectedShape { get; set; }
        string FilePath { get; set; }
        DrawMode DrawMode { get; set; }
        EditGeometry.Action EditGeometryAction { get; set; }

        ushort NewTaskShapeId { get; set; }

        void AddShape(Shape shape);
        void RemoveShape(int shapeId);
        void RemoveAllShape();
        void SelectedShapesItemsSource(int taskId);

    }
}
