using System.Globalization;
using Avalonia.Data.Converters;
using Avalonia.Media;

namespace AvaloniaBlog.Lib.Converts;

public class ColorAlphaConvert : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        var alpha = System.Convert.ToByte(parameter);
        if (value is ISolidColorBrush solidColorBrush && alpha != 0 && solidColorBrush.Color.A != alpha)
        {
            var color = solidColorBrush.Color;
            return new SolidColorBrush(new Color(alpha, color.R, color.G, color.B));
        }

        return value;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}