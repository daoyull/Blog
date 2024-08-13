using Avalonia.Controls;
using BlogView.Service;
using Common.Mvvm.Abstracts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BlogView.ViewModels;

public partial class MainViewModel : BaseViewModel
{
    private readonly ScrollViewerService _scrollViewerService;

    public MainViewModel(ScrollViewerService scrollViewerService)
    {
        _scrollViewerService = scrollViewerService;
        scrollViewerService.ScrollChanged += HandlerScrollChanged;
    }

    private async void HandlerScrollChanged()
    {
        var vector = _scrollViewerService.ScrollViewer.Offset;
        if (vector.Y > ToTopShowHeight && !ToTopImageIsShow)
        {
            ToTopImageOpacity = 1;
            ToTopImageIsShow = true;
        }

        if (vector.Y <= ToTopShowHeight && ToTopImageIsShow)
        {
            ToTopImageOpacity = 0;
            // 等图像Opacity动画结束，隐藏按钮
            await Task.Delay(200);
            ToTopImageIsShow = false;
        }
    }

    /// <summary>
    /// 主窗体
    /// </summary>
    [ObservableProperty] private Control? _mainView;


    /// <summary>
    /// 图像透明度
    /// </summary>
    [ObservableProperty] private double _toTopImageOpacity = 100;

    /// <summary>
    /// 图像是否课件
    /// </summary>
    [ObservableProperty] private bool _toTopImageIsShow = false;

    private const double ToTopShowHeight = 300;


    [RelayCommand]
    private void ToTop()
    {
        _scrollViewerService.ChangeOffset(0,400);
    }
    
}
