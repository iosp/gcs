using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using GCSArduino.Interface;

namespace GCSArduino.Controls
{
    public class BorderDropAndDrop : Border, IDropable, IDragable
    {
        public Type DataType
        {
            get { return typeof(BorderDropAndDrop); }
        }

        void IDropable.Drop(object data)
        {
            var uiElement = data as UIElement;
            if (uiElement != null)
                Child = uiElement;
            else
            {
                DataContext = data;
            }
        }
        public void Remove(object i)
        {
            Child = null;
        }
        public void Add(object uIElement, object dataContext)
        {
            Child = (UIElement)uIElement;
            DataContext = dataContext;
        }
        protected override void OnGiveFeedback(GiveFeedbackEventArgs e)
        {
            base.OnGiveFeedback(e);

            if (e.Effects.HasFlag(DragDropEffects.Copy))
            {
                Mouse.SetCursor(Cursors.Cross);
            }
            else if (e.Effects.HasFlag(DragDropEffects.Move))
            {
                Mouse.SetCursor(Cursors.Cross);
            }
            else
            {
                Mouse.SetCursor(Cursors.No);
            }
            e.Handled = true;
        }
    }
}
