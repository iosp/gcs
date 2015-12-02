using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GCSArduino.Interface;
using GCSArduino.Model;
using Interfaces;
using Microsoft.Practices.ServiceLocation;
using MissionPackage;


namespace GCSArduino.View
{
    /// <summary>
    /// Interaction logic for LayersControl.xaml
    /// </summary>
    public partial class LayersControl : UserControl
    {
        public LayersControl()
        {
            InitializeComponent();
        }

        private void TreeViewQuad_OnSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var treeView = (TreeView)sender;
            var selectedSectionTask = treeView.SelectedItem as Task;
            if (selectedSectionTask != null)
            {
                ServiceLocator.Current.GetInstance<IMap>().SelectedShapesItemsSource(selectedSectionTask.TaskID);
            }
            else
            {
                var guiQuadVehicle = treeView.SelectedItem as GuiQuadVehicle;
                if (guiQuadVehicle != null)
                {
                    var vehicles = ServiceLocator.Current.GetInstance<IVehiclesSource>().Vehicles;
                    foreach (GuiQuadVehicle vehicle in vehicles)
                    {
                        if (vehicle.ID != guiQuadVehicle.ID)
                            vehicle.IsSelected = false;
                    }
                    guiQuadVehicle.IsSelected = !guiQuadVehicle.IsSelected;
                    ServiceLocator.Current.GetInstance<IMap>().SelectedShapesItemsSource(guiQuadVehicle.Task.TaskID);
                }
            }
        }
    }
}
