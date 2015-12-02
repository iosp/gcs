using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Common.Class;

namespace GCSArduino.View
{
    /// <summary>
    /// Interaction logic for ClientQuadUI.xaml
    /// </summary>
    public partial class ClientQuadUI 
    {
        //public VideoBase VideoBase1 { get; set; }
        //public VideoBase VideoBase2 { get; set; }
        public ClientQuadUI()
        {
            InitializeComponent();
            //Loaded += ClientQuadUI_Loaded;
            
        }

        //void ClientQuadUI_Loaded(object sender, RoutedEventArgs e)
        //{
        //    VideoBase1 = new VideoLanGst("", 5000, (uint)(uint)videoPanel1.Handle, ConfigurationManager.AppSettings["VideoDirectory"]);
        //    VideoBase1.StartVideo();
        //    VideoBase2 = new VideoLanGst("", 5001, (uint)(uint)videoPanel2.Handle, ConfigurationManager.AppSettings["VideoDirectory"]);
        //    VideoBase2.StartVideo();
        //}
        
        //public void Dispose()
        //{
        //    VideoBase1.StopVideo();
        //    VideoBase2.StopVideo();
        //}
    }
}
