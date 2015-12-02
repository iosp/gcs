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
using System.Windows.Threading;

namespace GCSArduino.View
{
    /// <summary>
    /// Interaction logic for StatusMenuView.xaml
    /// </summary>
    public partial class StatusMenuView : UserControl
    {
        private DispatcherTimer _dispatcherUiTimer;
        private const int UpdataTimer = 100;

        public StatusMenuView()
        {
            InitializeComponent();
            _dispatcherUiTimer = new DispatcherTimer { Interval = new TimeSpan(0, 0, 0, 0, UpdataTimer) };
            _dispatcherUiTimer.Tick += DispatcherUiTimerTick;
            _dispatcherUiTimer.Start();
        }

        private void DispatcherUiTimerTick(object sender, EventArgs e)
        {
            Time.Text = DateTime.Now.ToString("HH:mm:ss");
        }
    }
}
