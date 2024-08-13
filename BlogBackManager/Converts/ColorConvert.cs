using System.Globalization;
using Avalonia.Data;
using Avalonia.Data.Converters;
using Avalonia.Media;

namespace BlogBackManager.Converts;

public class ColorConvert : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {

        if (value is string hexString && targetType.IsAssignableTo(typeof(Color)))
        {
            var color = Color.Parse(hexString);
            return color;
        }
        return new BindingNotification(new InvalidCastException(), 
            BindingErrorType.Error);
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is Avalonia.Media.Color color && targetType.IsAssignableTo(typeof(string)))
        {
            return color.ToString();
        }
        return new BindingNotification(new InvalidCastException(), BindingErrorType.Error);
    }
}