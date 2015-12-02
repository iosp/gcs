using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace GCSArduino.Converter
{
    class SignalStrengthColorConverter : IValueConverter
    {
        #region Implementation of IValueConverter
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                var val = System.Convert.ToInt32(value.ToString());
                var param = System.Convert.ToInt32(parameter.ToString());
                if(val <= 40 && param <=40)
                    return Brushes.Red;
                if (val >= param) 
                    return Brushes.GreenYellow;
                return Brushes.White;
            }
            catch
            {
                return Brushes.White;
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
