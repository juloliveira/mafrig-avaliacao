using System;
using System.Globalization;
using System.Windows.Data;

namespace Mafrig.Avaliacao.WPF
{
    public class DateTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return null;
            var date = System.Convert.ToDateTime(value);
            return $"{date:dd/MM/yyyy}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                if (value == null) return (DateTime?)null;
                return System.Convert.ToDateTime(value);
            }
            catch
            {
                return null;
            }
        }
    }
}
