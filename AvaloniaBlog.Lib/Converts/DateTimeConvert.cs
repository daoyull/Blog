using System.Globalization;
using Avalonia.Data.Converters;

namespace AvaloniaBlog.Lib.Converts;

public class DateTimeConvert : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is DateTime dateTime)
        {
            if (parameter is string format)
            {
                return dateTime.ToString(format);
            }
            else
            {
                return dateTime.ToString("yyyy-MM-dd HH:mm:ss");
            }
        }

        return value;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}