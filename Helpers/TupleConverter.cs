using System;
using System.Globalization;
using System.Windows.Data;

namespace JSON_Editor.Helpers
{
    public class TupleConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            // Assuming two string values
            return Tuple.Create(values[0]?.ToString(), values[1]?.ToString());
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}