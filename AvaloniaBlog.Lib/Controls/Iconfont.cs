using System.Collections.Concurrent;
using System.Reflection;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Layout;
using Avalonia.Media;
using AvaloniaBlog.Lib.Attritubes;
using AvaloniaBlog.Lib.Entity;

namespace AvaloniaBlog.Lib.Controls;

public class Iconfont : AvaloniaObject
{
    private static readonly ConcurrentDictionary<Icon, string> IconDic = new();

    static Iconfont()
    {
        IconProperty.Changed.AddClassHandler<TextBlock>(HandleIconChanged);
    }

    static readonly FontFamily Family = new("avares://AvaloniaBlog.Lib/Assets/Fonts#iconfont");

    #region Icon

    private static void HandleIconChanged(TextBlock arg1, AvaloniaPropertyChangedEventArgs arg2)
    {
        if (arg2.NewValue is not Icon icon)
        {
            return;
        }

        if (IconDic.TryGetValue(icon, out var iconStr) && !string.IsNullOrEmpty(iconStr))
        {
            arg1.FontFamily = Family;
            arg1.Text = iconStr;
            return;
        }

        var iconMark = icon.GetType().GetField(Enum.GetName(icon)!)!.GetCustomAttribute<IconMark>();
        if (!string.IsNullOrEmpty(iconMark?.Name))
        {
            arg1.FontFamily = Family;
            arg1.Text = iconMark.Name;
            IconDic[icon] = iconMark.Name;
        }
    }

    public static readonly AttachedProperty<Icon> IconProperty =
        AvaloniaProperty.RegisterAttached<Iconfont, TextBlock, Icon>(
            "Icon", Icon.Null, false, BindingMode.OneTime);

    public static void SetIcon(AvaloniaObject element, Icon iconValue)
    {
        element.SetValue(IconProperty, iconValue);
    }

    public static Icon GetIcon(AvaloniaObject element)
    {
        return element.GetValue(IconProperty);
    }

    #endregion


    #region IconStr

    private static void HandleIconStrChanged(TextBlock arg1, AvaloniaPropertyChangedEventArgs arg2)
    {
        if (arg2.NewValue is string iconStr && !string.IsNullOrEmpty(iconStr))
        {
            arg1.FontFamily = Family;
            arg1.HorizontalAlignment = HorizontalAlignment.Center;
            arg1.VerticalAlignment = VerticalAlignment.Center;
            arg1.Text = iconStr;
        }
    }

    public static readonly AttachedProperty<string> IconStrProperty =
        AvaloniaProperty.RegisterAttached<Iconfont, TextBlock, string>(
            "IconStr", "", false, BindingMode.OneTime);

    public static void SetIconStr(AvaloniaObject element, string iconStrValue)
    {
        element.SetValue(IconStrProperty, iconStrValue);
    }

    public static string GetIconStr(AvaloniaObject element)
    {
        return element.GetValue(IconStrProperty);
    }

    #endregion
}