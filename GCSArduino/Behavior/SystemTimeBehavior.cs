using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Interactivity;
using System.Windows.Threading;

namespace GCSArduino.Behavior
{
    public class SystemTimeBehavior : Behavior<TextBlock>
    {
        private  DispatcherTimer _dispatcherUiTimer;
        private const int UpdataTimer = 100;

        protected override void OnAttached()
        {
            base.OnAttached();
            _dispatcherUiTimer = new DispatcherTimer { Interval = new TimeSpan(0, 0, 0, 0, UpdataTimer) };
            _dispatcherUiTimer.Tick += DispatcherUiTimerTick;
            _dispatcherUiTimer.Start();
        }

        private void DispatcherUiTimerTick(object sender, EventArgs e)
        {
            AssociatedObject.Text = DateTime.Now.ToString("HH:mm:ss");
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            _dispatcherUiTimer.Tick -= DispatcherUiTimerTick;
            _dispatcherUiTimer.Stop();
        }
    }
}
