using System;
using System.Globalization;
using System.Windows.Data;

namespace WpfApp10
{
    public class BoolToStringConverter : IValueConverter // return connected to network  if true or disconnected if not
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool booleanValue)
            {
                return booleanValue ? "Connected" : "Disconnected";
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
// source property: boolean (IsConnectedToNetwork) and the target property is a string