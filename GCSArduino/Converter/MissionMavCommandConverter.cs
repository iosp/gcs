using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using GCSArduino.Enums;

namespace GCSArduino.Converter
{
    class MissionMavCommandConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            ushort val = (ushort)value;

            return (MissionMavCmd) val;
        }

        public object ConvertBack(object value, Type targetType, object parameter,
                                  System.Globalization.CultureInfo culture)
        {
            var v = (MissionMavCmd) value;
            var s = (ushort) v;
            return s;
        }
    }
}
