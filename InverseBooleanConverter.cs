using System;
using System.Globalization;
using System.Windows.Data;

namespace WpfApp10
{
    public class InverseBooleanConverter : IValueConverter // returns false if the input is true and vice versa.
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool booleanValue)
            {
                return !booleanValue;
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

// if IsOn is true (the lamp is Turned on), IsEnabled will be false (the button is disabled/greyed out), and vice versa.