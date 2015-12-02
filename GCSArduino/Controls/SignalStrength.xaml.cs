using System.Windows;

namespace GCSArduino.Controls
{
    /// <summary>
    /// Interaction logic for SignalStrength.xaml
    /// </summary>
    public partial class SignalStrength 
    {
        public static readonly DependencyProperty StateProperty =
            DependencyProperty.Register("State", typeof(int), typeof(SignalStrength), new PropertyMetadata(StatePropertyChanged));

        private static void StatePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var md = d as SignalStrength;
            if (md != null)
                md.State = (int) e.NewValue;
        }

        public int State
        {
            get { return (int)GetValue(StateProperty); }
            set { SetValue(StateProperty, value); }
        }
        public SignalStrength()
        {
            InitializeComponent();
        }
    }
}
