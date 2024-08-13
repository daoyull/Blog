using System;
using System.Collections.Generic;
using Avalonia.Media;

namespace BlogView.Helpers;

public static class RandomColorHelper
{
    static RandomColorHelper()
    {
        _colors.Add(new ColorItem("#6E40AA"));
        _colors.Add(new ColorItem("#963DAB"));
        _colors.Add(new ColorItem("#BF3CBF"));
        _colors.Add(new ColorItem("#E4419D"));
        _colors.Add(new ColorItem("#FE4B83"));
        _colors.Add(new ColorItem("#FF5E63"));
        _colors.Add(new ColorItem("#FF7847"));
        _colors.Add(new ColorItem("#FB9633"));
        _colors.Add(new ColorItem("#E2B72F"));
        _colors.Add(new ColorItem("#C6D63C"));
        _colors.Add(new ColorItem("#AFED5B"));
        _colors.Add(new ColorItem("#7FFF58"));
        _colors.Add(new ColorItem("#52F667"));
        _colors.Add(new ColorItem("#30EF82"));
        _colors.Add(new ColorItem("#1DDEA3"));
        _colors.Add(new ColorItem("#1AB3C2"));

        _lastColor = _colors[0];
    }

    private static readonly List<ColorItem> _colors = new();

    private static Random _random = new();

    private static ColorItem _lastColor;

    public static SolidColorBrush GetRandomColor()
    {
        while (true)
        {
            var next = _random.Next(_colors.Count);
            var color = _colors[next];
            if (color != _lastColor)
            {
                _lastColor = color;
                return color.ColorBrush;
            }
        }
    }
    
    public static string GetRandomColorStr()
    {
        while (true)
        {
            var next = _random.Next(_colors.Count);
            var color = _colors[next];
            if (color != _lastColor)
            {
                _lastColor = color;
                return color.ColorStr;
            }
        }
    }
}

class ColorItem
{
    public ColorItem(string color)
    {
        ColorStr = color;
        ColorBrush = new SolidColorBrush(Color.Parse(ColorStr));
    }

    public string ColorStr { get; set; }

    public SolidColorBrush ColorBrush { get; set; }
}