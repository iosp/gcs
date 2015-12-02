using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using Common.Enums;
using GCSArduino.Enums;

namespace GCSArduino.Converter
{
    public class GpsStatusColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                var val = (GpsStatusEnum)value;
                switch (val)
                {
                    case GpsStatusEnum.NoFix:
                        return Brushes.Red;

                    case GpsStatusEnum.TwoD:
                        return Brushes.Orange;

                    case GpsStatusEnum.ThreeD:
                        return Brushes.GreenYellow;
                    default:
                        return Brushes.White;
                }
            }
            catch
            {
                return  Brushes.White;
            }
           
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
