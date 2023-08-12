using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeZooiNaCrypto.Util
{
    public class ValueToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return null;
            Type type = value.GetType();

            if (type == typeof(decimal))
            {
                decimal decimalValue = (decimal)value;

                if (decimalValue > 0) { return Color.FromArgb("#039C23"); }
                else if (decimalValue < 0) { return Color.FromArgb("#f44336"); }
                else { return Color.FromArgb("#141414"); }

            } else if (type == typeof(int))
            {
                int intValue = (int)value;

                if (intValue > 0) { return Color.FromArgb("#039C23"); }
                else if (intValue < 0) { return Color.FromArgb("#f44336"); }
                else { return Color.FromArgb("#141414"); }
            }

                return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
