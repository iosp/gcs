using System;
using System.Configuration;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using Common.Class;
using GCSArduino.Messengers;
using HwndExtensions.Host;
using Application = System.Windows.Application;

namespace GCSArduino.Model
{
    public class VideoPresenter : WindowsFormsHost //HwndHostPresenter 
    {
        public static readonly DependencyProperty PortProperty =
            DependencyProperty.Register("Port", typeof (int), typeof (VideoPresenter), new PropertyMetadata(default(int)));

        public int Port
        {
            get { return (int) GetValue(PortProperty); }
            set { SetValue(PortProperty, value); }
        }

        VideoBase VideoBase { get; set; }

        //WindowsFormsHost Browser { get; set; }

        Panel Panel { get; set; }

        public VideoPresenter( )
        {
            
        }

        public void Initialize()
        {
            //Browser = new WindowsFormsHost();
            Panel = new Panel();
            //Browser.Child = Panel;
            Child = Panel;

            //HwndHost = Browser;

            RegisterToAppShutdown();

            VideoBase = new VideoLanGst("", Port, (uint)Panel.Handle, ConfigurationManager.AppSettings["VideoDirectory"]);
            VideoBase.StartVideo();
        }

        private void VehicleSelectedAction(VehicleSelected obj)
        {

            //if (obj.Vehicle == null) return;
          
            //GuiQuadVehicleSelected = obj.Vehicle as GuiQuadVehicle;
          
            //if (GuiQuadVehicleSelected != null && GuiQuadVehicleSelected != OldGuiQuadVehicleSelected)
            //{
            //    if (OldGuiQuadVehicleSelected != null && OldGuiQuadVehicleSelected.VideoBase != null)
            //    {
            //        OldGuiQuadVehicleSelected.VideoBase.StopVideo();
            //        OldGuiQuadVehicleSelected.VideoBase = null;
            //    }
            //    GuiQuadVehicleSelected.VideoBase = new VideoLanGst("", 5000, (uint)panel.Handle,
            //                                                       ConfigurationManager.AppSettings["VideoDirectory"]);
            //    // GuiQuadVehicleSelected.VideoBase.StartRecord("Video_Quad" + guiVehicle.ID + "_");
            //    GuiQuadVehicleSelected.VideoBase.StartVideo();
            //    OldGuiQuadVehicleSelected = GuiQuadVehicleSelected;
            //}

        }

        private void RegisterToAppShutdown()
        {
            Application.Current.Dispatcher.ShutdownStarted += OnShutdownStarted;
        }

        private void OnShutdownStarted(object sender, EventArgs e)
        {
            if (VideoBase != null)
                VideoBase.StopVideo();
            Dispose();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //var host = HwndHost;
                //VideoBase.StopVideo();
                VideoBase = null;
                //if(host != null)
                //    host.Dispose();
            }
        }
    }
}
