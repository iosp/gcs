using System;
using System.Globalization;
using System.Windows.Data;
using Common.Enums;
using MMAVLink;

namespace GCSArduino.Converter
{
    public class QuadModeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                var mode = (MAVLink.MAV_MODE_FLAG)value;
                if (mode != null)
                    return (StringToEnum<VehicleModeEnum>(value.ToString())).GetDescription(); // <-- The extention method
                return "Unknown"; // <-- The extention method

            }
            catch (Exception)
            {
                return "Unknown"; // <-- The extention method
            }


        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
        public static T StringToEnum<T>(string name)
        {
            return (T)Enum.Parse(typeof(T), name);
        }
    }
}
