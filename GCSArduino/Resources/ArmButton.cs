using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using GCSArduino.Enums;


namespace GCSArduino.Resources
{
    public class ArmToggleButton : Button
    {
        public static readonly DependencyProperty ArmStatusProperty =
            DependencyProperty.Register("ArmStatus", typeof(ArmStatusEnum), typeof(ArmToggleButton), new PropertyMetadata(default(ArmStatusEnum)));

        public ArmStatusEnum ArmStatus
        {
            get { return (ArmStatusEnum) GetValue(ArmStatusProperty); }
            set { SetValue(ArmStatusProperty, value); }
        }   
    }
}
