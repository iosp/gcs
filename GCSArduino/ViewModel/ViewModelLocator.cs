using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GCSArduino.Interface;
using GCSArduino.Model;
using GalaSoft.MvvmLight;
using Interfaces;
using Microsoft.Practices.ServiceLocation;
using GalaSoft.MvvmLight.Ioc;

namespace GCSArduino.ViewModel
{
    public class ViewModelLocator
    {
        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(()=> SimpleIoc.Default);
            if (ViewModelBase.IsInDesignModeStatic)
            {
                
            }
            else
            {
                //SimpleIoc.Default.Register<IStatus,StatusViewModel>();
                //SimpleIoc.Default.Register<IButtonsControl, ButtonsControlViewModel>();
                //SimpleIoc.Default.Register<IComm, UdpSerial>();
                //SimpleIoc.Default.Register<IRightCommandsControl, RightCommandsControlViewModel>();
                //SimpleIoc.Default.Register<ILeftCommandsControl, LeftCommandsControlViewModel>();
                //SimpleIoc.Default.Register<IArduCopterMode, ArduCopterModeViewModel>();
                //SimpleIoc.Default.Register<IQuadVideo, QuadVideoViewModel>();
                SimpleIoc.Default.Register<IVehicleComponent, VehicleComponent.VehicleComponent.VehicleComponent>();
                SimpleIoc.Default.Register<IMissionComponent, MissionComponent.MissionComponent.MissionComponent>();

                SimpleIoc.Default.Register<IMissionSource, MissionSource>();
                SimpleIoc.Default.Register<IVehiclesSource, VehiclesSource>();

                

                bool server = bool.Parse(ConfigurationManager.AppSettings["IF_RUN_SERVER_QUADS"]);

              
                SimpleIoc.Default.Register<IMap, MapViewModel>();
                SimpleIoc.Default.Register<IListWaypoint, ListWaypointViewModel>();
                SimpleIoc.Default.Register<IStatusMenuView, StatusMenuControlViewModel>();
                SimpleIoc.Default.Register<IInstrumentControl, InstrumentControlViewModel>();
                SimpleIoc.Default.Register<IJoystick, JoystickViewModel>();
                SimpleIoc.Default.Register<IQuadProperty, QuadPropertyViewModel>();
                SimpleIoc.Default.Register<IToolsControl, ToolsControlViewModel>();
                SimpleIoc.Default.Register<IMenuViewModel, MenuViewModel>();
                SimpleIoc.Default.Register<ILayerControl, LayerControlViewModel>();
                SimpleIoc.Default.Register<IToolAction, QuadToolActionViewModel>();
                SimpleIoc.Default.Register<IClientQuadUI, ClientQuadUIViewModel>();
                SimpleIoc.Default.Register<IToolActionMap, ToolActionMapViewModel>();
                SimpleIoc.Default.Register<IQuadsStatus, QuadsStatusViewModel>();
                SimpleIoc.Default.Register<IMaintenance, MaintenanceViewModel>();
               
            }
        }

      
        public IMap MapViewModel
        {
            get { return SimpleIoc.Default.GetInstance<IMap>(); }
        }
        public IQuadProperty QuadPropertyViewModel
        {
            get { return SimpleIoc.Default.GetInstance<IQuadProperty>(); }
        }
        //public IButtonsControl ButtonsControlViewModel
        //{
        //    get { return SimpleIoc.Default.GetInstance<IButtonsControl>(); }
        //}
        public IListWaypoint ListWaypointViewModel
        {
            get { return SimpleIoc.Default.GetInstance<IListWaypoint>(); }
        }

        //public ILeftCommandsControl LeftCommandsControlViewModel
        //{
        //    get { return SimpleIoc.Default.GetInstance<ILeftCommandsControl>(); }
        //}

        //public IRightCommandsControl RightCommandsControlViewModel
        //{
        //    get { return SimpleIoc.Default.GetInstance<IRightCommandsControl>(); }
        //}
        public IStatusMenuView StatusMenuControlViewModel
        {
            get { return SimpleIoc.Default.GetInstance<IStatusMenuView>(); }
        }

        public IVehiclesSource VehiclesSource
        {
            get { return SimpleIoc.Default.GetInstance<IVehiclesSource>(); }
        }
        public IInstrumentControl InstrumentControlViewModel
        {
            get { return SimpleIoc.Default.GetInstance<IInstrumentControl>(); }
        }
        public IJoystick JoystickViewModel
        {
            get { return SimpleIoc.Default.GetInstance<IJoystick>(); }
        }
        //public IArduCopterMode ArduCopterModeViewModel
        //{
        //    get { return SimpleIoc.Default.GetInstance<IArduCopterMode>(); }
        //}
        public IMissionSource MissionSource
        {
            get { return SimpleIoc.Default.GetInstance<IMissionSource>(); }
        }
        //public IQuadVideo QuadVideoViewModel
        //{
        //    get { return SimpleIoc.Default.GetInstance<IQuadVideo>(); }
        //}
        public IToolsControl ToolsControlViewModel
        {
            get { return SimpleIoc.Default.GetInstance<IToolsControl>(); }
        }
        public ILayerControl LayerControlViewModel
        {
            get { return SimpleIoc.Default.GetInstance<ILayerControl>(); }
        }
        public IToolAction QuadToolActionViewModel
        {
            get { return SimpleIoc.Default.GetInstance<IToolAction>(); }
        }

        public IMenuViewModel MenuViewModel
        {
            get { return SimpleIoc.Default.GetInstance<IMenuViewModel>(); }
        }
        public IToolActionMap ToolActionMapViewModel
        {
            get { return SimpleIoc.Default.GetInstance<IToolActionMap>(); }
        }
        public IClientQuadUI ClientQuadUIViewModel
        {
            get { return SimpleIoc.Default.GetInstance<IClientQuadUI>(); }
        }
        public IQuadsStatus QuadsStatusViewModel
        {
            get { return SimpleIoc.Default.GetInstance<IQuadsStatus>(); }
        }
        public IMaintenance MaintenanceViewModel
        {
            get { return SimpleIoc.Default.GetInstance<IMaintenance>(); }
        }

        
    }
}
