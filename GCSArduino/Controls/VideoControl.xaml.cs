using System;
using System.Collections.Generic;
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

namespace GCSArduino.Controls
{
    /// <summary>
    /// Interaction logic for VideoControl.xaml
    /// </summary>
    public partial class VideoControl : UserControl
    {
        public static readonly DependencyProperty PortProperty =
            DependencyProperty.Register("Port", typeof (int), typeof (VideoControl), new PropertyMetadata(default(int)));

        public int Port
        {
            get { return (int) GetValue(PortProperty); }
            set { SetValue(PortProperty, value); }
        }   

        public VideoControl()
        {
            InitializeComponent();
            Loaded += VideoControl_Loaded;
        }

        void VideoControl_Loaded(object sender, RoutedEventArgs e)
        {
            VideoPresenter.Port = Port;
            VideoPresenter.Initialize();
        }
    }
}
