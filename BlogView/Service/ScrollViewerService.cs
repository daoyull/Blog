using Avalonia;
using Avalonia.Animation;
using Avalonia.Animation.Easings;
using Avalonia.Controls;
using Microsoft.Extensions.Logging;

namespace BlogView.Service;

/// <summary>
/// 博客项目滚动条服务
/// </summary>
public class ScrollViewerService
{
    private readonly ILogger<ScrollViewerService> _logger;

    public ScrollViewerService(ILogger<ScrollViewerService> logger)
    {
        _logger = logger;
    }

    public Action? ScrollChanged { get; set; }

    public bool IsAnimation { get; private set; }

    /// <summary>
    /// 滚动时的动画
    /// </summary>
    // private readonly VectorTransition _vectorTransition = new VectorTransition()
    // {
    //     Property = ScrollViewer.OffsetProperty
    // };

    public ScrollViewer ScrollViewer { get; set; } = null!;

    public void Start()
    {
        ScrollViewer.ScrollChanged += HandleScrollChanged;
        if (ScrollViewer.Transitions == null)
        {
            ScrollViewer.Transitions = new Transitions();
        }

        // _vectorTransition.Easing = new SineEaseIn();
        // ScrollViewer.Transitions.Add(_vectorTransition);
    }

    /// <summary>
    /// 改变Y轴进度条
    /// </summary>
    /// <param name="y">y坐标</param>
    /// <param name="time">动画时间(</param>
    // public async void ChangeOffset(double y, double time = 0.4)
    // {
    //     IsAnimation = true;
    //     _vectorTransition.Duration = TimeSpan.FromSeconds(time);
    //     await Dispatcher.UIThread.Invoke(async () =>
    //     {
    //         ScrollViewer.Offset = new Vector(0, y);
    //         await Task.Delay(_vectorTransition.Duration);
    //     });
    //     
    //     IsAnimation = false;
    //     _vectorTransition.Duration = TimeSpan.FromMicroseconds(0);
    //     // 完成后通知界面更新
    //     NotifyScrollChanged();
    // }
    private const int Interval = 20;

    /// <summary>
    /// 改变Y轴进度条
    /// </summary>
    /// <param name="y">y坐标</param>
    /// <param name="time">动画时间(</param>
    public async void ChangeOffset(double y, long time = Interval)
    {
        if (IsAnimation)
        {
            return;
        }

        IsAnimation = true;


        var endOffset = new Vector(0, y);
        var startOffset = ScrollViewer.Offset;
        double totalTime = time;
        var sineEaseIn = new SineEaseIn();
        long currentTime = 0;
        while (true)
        {
            if (currentTime > totalTime)
            {
                break;
            }

            var ratio = currentTime / totalTime;
            var offset = (endOffset - startOffset) * sineEaseIn.Ease(ratio) + startOffset;
            // ScrollViewer.SetCurrentValue(ScrollViewer.OffsetProperty,offset);
            ScrollViewer.Offset = offset;
            currentTime += Interval;
            await Task.Delay(Interval);
        }

        IsAnimation = false;

        // 完成后通知界面更新
        NotifyScrollChanged();
    }

    /// <summary>
    /// 计算控件在ScrollViewer中的位置
    /// </summary>
    /// <param name="control"></param>
    /// <returns></returns>
    public double? CalculateLocation(Control? control)
    {
        if (control == null)
        {
            return null;
        }

        var point = control.TranslatePoint(new Point(), ScrollViewer);
        return point?.Y;
    }

    private void HandleScrollChanged(object? sender, ScrollChangedEventArgs args)
    {
        if (sender is not ScrollViewer scrollViewer)
        {
            return;
        }

        NotifyScrollChanged();
    }

    private void NotifyScrollChanged()
    {
        _logger.LogDebug(string.Format("Offset Changed: {0}", ScrollViewer.Offset));
        // 转发一下
        ScrollChanged?.Invoke();
    }

    public void Stop()
    {
        ScrollViewer.ScrollChanged -= HandleScrollChanged;
    }
}