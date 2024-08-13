using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace BlogView.Converts;

public class MonentsHeightConvert : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is double height && parameter != null && double.TryParse(parameter.ToString(), out var addHeight))
        {
            return height + addHeight;
        }

        return value;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}