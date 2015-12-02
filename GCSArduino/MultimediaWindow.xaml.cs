using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Forms.Integration;
using Common.Class;
using GCSArduino.Messengers;
using GCSArduino.Model;
using GalaSoft.MvvmLight.Messaging;
using Interfaces;
using Microsoft.Practices.ServiceLocation;

namespace GCSArduino
{
    /// <summary>
    /// Interaction logic for MultimediaWindow.xaml
    /// </summary>
    public partial class MultimediaWindow : Window
    {
        public Dictionary<int, int> VideoMatrix = new Dictionary<int, int>();

        public MultimediaWindow()
        {
            InitializeComponent();
            Closed += MultimediaWindow_Closed;
            Messenger.Default.Register<NewVehicleMessenger>(this, NewVehicleMessengerFunction);
            Messenger.Default.Register<ClosedWindowEventArgsMessenger>(this,ClosedWindowEventArgsMessengerFunction);
            InitializeVideo();
        }

        private void ClosedWindowEventArgsMessengerFunction(ClosedWindowEventArgsMessenger obj)
        {
            Close();
        }

        void MultimediaWindow_Closed(object sender, EventArgs e)
        {
            var vehicles = ServiceLocator.Current.GetInstance<IVehiclesSource>().Vehicles;

            foreach (var guiVehicle in vehicles.Select(vehicle => vehicle as GuiQuadVehicle))
            {
                if (guiVehicle.VideoBase != null)
                guiVehicle.VideoBase.StopVideo();
            }
        }

        private void NewVehicleMessengerFunction(NewVehicleMessenger obj)
        {
            Application.Current.Dispatcher.Invoke(InitializeVideo);
        }

        static int VideoIdx = 0;

        public void InitializeVideo()
        {
           VideoMatrix.Clear();
            for (int i = 0; i < MultimediaGrid.ColumnDefinitions.Count; i++)
            {
                for (int j = 0; j < MultimediaGrid.RowDefinitions.Count; j++)
                {
                    var windowsFormsHost = MultimediaGrid.Children[i * (MultimediaGrid.RowDefinitions.Count) + j] as WindowsFormsHost;
                    VideoMatrix.Add(i * (MultimediaGrid.RowDefinitions.Count) + j, (int) windowsFormsHost.Child.Handle);
                }   
            }
            var vehicles = ServiceLocator.Current.GetInstance<IVehiclesSource>().Vehicles;
           
            foreach (var vehicle in vehicles)
            {
               
                var guiVehicle = vehicle as GuiQuadVehicle;
                if (guiVehicle != null)
                {
                    if (guiVehicle.VideoBase == null)
                    {
                        guiVehicle.VideoBase = new VideoLanGst("", 5000,
                                                                   (uint)VideoMatrix[VideoIdx],
                                                                   ConfigurationManager.AppSettings["VideoDirectory"]);
                        Debug.WriteLine("init ID " + guiVehicle.ID + ":" + "VideoIdx=" + VideoIdx);
                        VideoIdx++;
                    }
                    if (!guiVehicle.VideoBase.IsCameraOpen)
                    {
                        guiVehicle.VideoBase.StartVideo();
                    }
                }
               
            }
            
        }
    }
}
