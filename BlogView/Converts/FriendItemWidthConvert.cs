using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace BlogView.Converts;

public class FriendItemWidthConvert : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is double width && parameter != null && double.TryParse(parameter.ToString(), out var addWidth))
        {
            return width / 3.2 + addWidth;
        }

        return value;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}