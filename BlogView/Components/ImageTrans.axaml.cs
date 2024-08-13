using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media;
using BlogView.Service;
using Common.Lib.Ioc;
using Microsoft.Extensions.Logging;

namespace BlogView.Components;

public partial class ImageTrans : UserControl
{
    private readonly ILogger<ImageTrans> _logger;
    private readonly PageService _pageService;
    private readonly ScrollViewerService _scrollViewerService;

    public ImageTrans()
    {
        InitializeComponent();
        _logger = Ioc.Resolve<ILogger<ImageTrans>>();
        _pageService = Ioc.Resolve<PageService>();
        _scrollViewerService = Ioc.Resolve<ScrollViewerService>();
    }

    private Brush? _topNavBackground;
    private Brush? _topBorderBrush;


    private void HandleScrollChangedMessageChanged()
    {
        if (!IsVisible || _scrollViewerService.IsAnimation)
        {
            return;
        }

        if (_scrollViewerService.ScrollViewer.Offset.Y > Bounds.Height / 2)
        {
            if (_topNavBackground != null) _topNavBackground.Opacity = 1;

            if (_topBorderBrush != null) _topBorderBrush.Opacity = 1;
        }
        else
        {
            if (_topNavBackground != null) _topNavBackground.Opacity = 0;
            if (_topBorderBrush != null) _topBorderBrush.Opacity = 0.08;
        }
    }

    private void InputElement_OnPointerMoved(object? sender, PointerEventArgs e)
    {
        if (sender is not Panel panel || _scrollViewerService.IsAnimation)
        {
            return;
        }

        var point = e.GetCurrentPoint(panel);
        var x = point.Position.X;
        var width = panel.Bounds.Width;
        var center = width / 2;
        var d = x - center;
        var scale = Math.Abs(d) / center;
        // 图像1
        Image1.Opacity = d >= 0 ? 0 : scale;
        // 图像2
        Image2.Opacity = scale > 0.9 ? 0 : 1 - scale;
        // 图像3
        Image3.Opacity = d <= 0 ? 0 : scale;
    }

    protected override void OnLoaded(RoutedEventArgs e)
    {
        var current = Application.Current;
        if (current == null)
        {
            return;
        }

        if (current.TryGetResource("TopNavBackground", current.ActualThemeVariant, out var color1) &&
            color1 is Brush brush1)
        {
            _topNavBackground = brush1;
        }

        if (current.TryGetResource("TopBorderBrush", current.ActualThemeVariant, out var color2) &&
            color2 is Brush brush2)
        {
            _topBorderBrush = brush2;
        }

        _pageService.ViewChanged += HandleViewChanged;
        _scrollViewerService.ScrollChanged += HandleScrollChangedMessageChanged;
        base.OnLoaded(e);
    }

    private void HandleViewChanged(ViewType obj)
    {
        if (obj == ViewType.Index)
        {
            IsVisible = true;
        }
        else
        {
            IsVisible = false;
            if (_topNavBackground != null) _topNavBackground.Opacity = 1;
            if (_topBorderBrush != null) _topBorderBrush.Opacity = 1;
        }
    }

    protected override void OnUnloaded(RoutedEventArgs e)
    {
        _pageService.ViewChanged -= HandleViewChanged;
        _scrollViewerService.ScrollChanged -= HandleScrollChangedMessageChanged;
        base.OnUnloaded(e);
    }

    private void Button_OnClick(object? sender, RoutedEventArgs e)
    {
        var calculateLocation = _scrollViewerService.CalculateLocation(App.MainView.MainBorder);
        _scrollViewerService.ChangeOffset(calculateLocation!.Value + _scrollViewerService.ScrollViewer.Offset.Y - 65, 600);
    }
}
