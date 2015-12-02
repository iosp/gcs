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
    class EqualiserColorConverter: IValueConverter

    {
        private static readonly Color V1 = Colors.Yellow;
        private static readonly Color V2 = Colors.GreenYellow;
        private static readonly Color V3 = Colors.Red;
        
        private static readonly Dictionary<int, SolidColorBrush> Dictionary = new Dictionary<int, SolidColorBrush>();
        
        public static void InitColor()
        {
            
            
        }

        public EqualiserColorConverter()
        {
            if (!Dictionary.ContainsKey(1))
            {
                Dictionary.Add(0, new SolidColorBrush(V1));
                Dictionary.Add(1, new SolidColorBrush(V2));
                Dictionary.Add(2, new SolidColorBrush(V3));
            }
        }
        #region Implementation of IValueConverter
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                var val = System.Convert.ToInt32(value.ToString());
                var param = System.Convert.ToInt32(parameter.ToString());

                const int stepNum = 2;
                const double max = 256;
                const double d = max/stepNum;

                if (val >= param)
                {
                    var div = (int) (param / d);
                    var factor = 1-Math.Abs (param - div * d) / d;

                    var color1 = Dictionary[div];
                    var color2 = Dictionary[div+1];

                    double alpha = factor * color1.Color.A + (1 - factor) * color2.Color.A;
                    double red = factor * color1.Color.R + (1 - factor) * color2.Color.R;
                    double green = factor * color1.Color.G + (1 - factor) * color2.Color.G;
                    double blue = factor * color1.Color.B + (1 - factor) * color2.Color.B;

                    Color color = Color.FromArgb((byte)alpha, (byte)red, (byte)green, (byte)blue);
                    var brush = new SolidColorBrush(color);
                    return brush;

                   
                }
                return Brushes.Transparent;
            }
            catch
            {
                return Brushes.White;
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Brushes.White;
        }
        #endregion
    }
}

