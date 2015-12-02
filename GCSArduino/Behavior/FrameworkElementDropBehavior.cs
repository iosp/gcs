using System;
using System.Windows.Interactivity;
using System.Windows;
using System.Windows.Controls;
using GCSArduino.Interface;

namespace GCSArduino.Behavior
{
    public class FrameworkElementDropBehavior : Behavior<Border>
    {
        private Type _dataType;
        private FrameworkElementAdorner _adorner;
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.AllowDrop = true;
            AssociatedObject.DragEnter += AssociatedObject_DragEnter;
            AssociatedObject.DragOver += AssociatedObject_DragOver;
            AssociatedObject.DragLeave += AssociatedObject_DragLeave;
            AssociatedObject.Drop += AssociatedObject_Drop;
        }

        void AssociatedObject_Drop(object sender, DragEventArgs e)
        {
            if (_dataType != null)
            {
                if (e.Data.GetDataPresent("View"))
                {
                    object sourceChild = null;
                    var border = (Border)sender;
                    object targetChild = border.Child;
                    object dataContext = border.DataContext;
                    var source = e.Data.GetData("View") as IDragable;
                    if (source != null)
                    {
                        sourceChild = e.Data.GetData("Child");
                        source.Remove(sourceChild);

                    }
                    var target = AssociatedObject as IDropable;
                    if (target != null)
                    {
                        target.Drop(sourceChild);
                        target.Drop(e.Data.GetData("DataContext"));
                    }

                    if (source != null)
                        source.Add(targetChild, dataContext);
                }
            }
            if (_adorner != null)
                _adorner.Remove();
            e.Handled = true;
        }

        void AssociatedObject_DragLeave(object sender, DragEventArgs e)
        {
            if (_adorner != null)
                _adorner.Remove();
            e.Handled = true;
        }

        void AssociatedObject_DragOver(object sender, DragEventArgs e)
        {
            if (_dataType != null)
            {
                if (e.Data.GetDataPresent(_dataType))
                {
                    SetDragDropEffects(e);

                    if (_adorner != null)
                        _adorner.Update();
                }
            }
        }
        private void SetDragDropEffects(DragEventArgs e)
        {
            e.Effects = DragDropEffects.None;
            if (e.Data.GetDataPresent(_dataType))
            {
                e.Effects = DragDropEffects.Move;
            }
        }
        void AssociatedObject_DragEnter(object sender, DragEventArgs e)
        {
            if (_dataType == null)
            {
                if (AssociatedObject != null)
                {
                    var dropObject = AssociatedObject as IDropable;
                    if (dropObject != null)
                    {
                        _dataType = dropObject.DataType;
                    }
                }
            }
            if (_adorner == null)
                _adorner = new FrameworkElementAdorner(sender as UIElement);
            e.Handled = true;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.DragEnter -= AssociatedObject_DragEnter;
            AssociatedObject.DragOver -= AssociatedObject_DragOver;
            AssociatedObject.DragLeave -= AssociatedObject_DragLeave;
            AssociatedObject.Drop -= AssociatedObject_Drop;
            base.OnDetaching();
        }
    }
}
