using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using Common.Utils;
using GCSArduino.Messengers;
using GCSArduino.Windows;
using GalaSoft.MvvmLight.Messaging;

namespace GCSArduino
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public Window Window { get; set; }
        public App()
        {
            string logFileName = ConfigurationManager.AppSettings["LogFileName"];
            Logger.InitLogger(logFileName);
            Logger.Info("Init Application Version - " + Assembly.GetExecutingAssembly().GetName().Version);
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            AppDomain.CurrentDomain.FirstChanceException += CurrentDomain_FirstChanceException;
            Dispatcher.UnhandledException += Dispatcher_UnhandledException;
            Current.DispatcherUnhandledException += Current_DispatcherUnhandledException;
            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;
        }

        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            bool serverMode = Convert.ToBoolean(ConfigurationManager.AppSettings["IF_RUN_SERVER_QUADS"]);
            if (serverMode)
                Window = new ServerWindow();
            else
                Window = new ClientWindow();
            
            Window.Title ="Ground Station Control Version " + Assembly.GetExecutingAssembly().GetName().Version;
            Window.Show();
            Window.Closed += MainWindow_Closed;

        }

        void MainWindow_Closed(object sender, System.EventArgs e)
        {
            Messenger.Default.Send(new ClosedWindowEventArgsMessenger());
        }

        void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            Logger.Error("TaskScheduler_UnobservedTaskException",e.Exception);
        }

        void Current_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            Logger.Error("Current_DispatcherUnhandledException", e.Exception);
        }

        void Dispatcher_UnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            Logger.Error("Dispatcher_UnhandledException", e.Exception);
        }

        void CurrentDomain_FirstChanceException(object sender, System.Runtime.ExceptionServices.FirstChanceExceptionEventArgs e)
        {
            Logger.Error("CurrentDomain_FirstChanceException", e.Exception);
        }

        void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Logger.Error("CurrentDomain_UnhandledException", e.ToString());
        }

       
    }
}
