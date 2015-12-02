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

namespace GCSArduino.Controls
{
    /// <summary>
    /// Interaction logic for AttitudeInstrumentControl.xaml
    /// </summary>
    public partial class AttitudeInstrumentControl : UserControl
    {
        public AttitudeInstrumentControl()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty AngleProperty =
            DependencyProperty.Register("Angle", typeof(double), typeof(AttitudeInstrumentControl), new PropertyMetadata(AnglePropertyChangedCallback));

        private static void AnglePropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var me = (AttitudeInstrumentControl)d;
            if (me != null)
                me.Angle = (double)e.NewValue;
        }

        public double Angle
        {
            get { return (double)GetValue(AngleProperty); }
            set { SetValue(AngleProperty, value); }
        }   
    }
}
