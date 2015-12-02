using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using GCSArduino.Enums;
using GCSArduino.Model;
using Interfaces;
using Microsoft.Practices.ServiceLocation;

namespace GCSArduino.Converter
{
    public class SelectedVehicleToArmConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var arm = (bool)value;
          
            return arm ? ArmStatusEnum.Arm : ArmStatusEnum.DisArm;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
