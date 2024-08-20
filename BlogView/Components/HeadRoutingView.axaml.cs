using System.Collections.ObjectModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using BlogView.Service;
using Common.Lib.Ioc;
using CommunityToolkit.Mvvm.ComponentModel;
using Markdig.Avalonia.Entity;

namespace BlogView.Components;

public partial class HeadRoutingView : UserControl
{
    private readonly ScrollViewerService _scrollViewerService;
    private List<RouterItem> _headingRouters = new();


    public readonly Control LocationControl = new() { Height = 1 };

    public HeadRoutingView()
    {
        _scrollViewerService = Ioc.Resolve<ScrollViewerService>();
        InitializeComponent();
    }


    private void InputElement_OnPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        if (sender is Border control && control.DataContext is RouterItem headingRouter)
        {
            var offset = _scrollViewerService.CalculateLocation(headingRouter.Control);
            if (offset == null)
            {
                return;
            }

            var offsetY = _scrollViewerService.ScrollViewer.Offset.Y + offset.Value -
                          headingRouter.Control.Bounds.Height;
            _scrollViewerService.ChangeOffset(offsetY, 400);
        }
    }

    public void HandleMarkViewLoaded(object? sender, List<HeadingRouter> obj)
    {
        _headingRouters = new List<RouterItem>(obj.Select(it => new RouterItem
        {
            Text = it.Text,
            Level = it.Level,
            Control = it.Control,
            IsExpand = false
        }));
        var headingRouters = HandleTree(_headingRouters);
        MainTreeView.ItemsSource = headingRouters;
        _scrollViewerService.ScrollChanged += HandleScrollChanged;
    }

    private List<RouterItem> HandleTree(List<RouterItem> items)
    {
        Stack<RouterItem> stack = new Stack<RouterItem>();
        List<RouterItem> rootItems = new List<RouterItem>();

        foreach (var item in items)
        {
            while (stack.Count > 0 && stack.Peek().Level >= item.Level)
            {
                stack.Pop();
            }

            if (stack.Count == 0)
            {
                rootItems.Add(item);
            }
            else
            {
                var routerItem = stack.Peek();
                routerItem.Items.Add(item);
                item.Parent = routerItem;
            }

            stack.Push(item);
        }

        return rootItems;
    }

    private void HandleScrollChanged()
    {
        var scrollServiceCurrentScrollOffset = _scrollViewerService.ScrollViewer.Offset;
        var locationPoint = _scrollViewerService.CalculateLocation(LocationControl);
        if (locationPoint == null)
        {
            return;
        }

        #region 定位高亮控件，Tree展开

        // 正在动画时暂停定位
        if (!_scrollViewerService.IsAnimation &&
            _scrollViewerService.ScrollViewer.Bounds.Height - (locationPoint - 80) > 0)
        {
            RouterItem? router = null;
            foreach (var headingRouter in _headingRouters)
            {
                var y = _scrollViewerService.CalculateLocation(headingRouter.Control);
                if (y < headingRouter.Control.Bounds.Height)
                {
                    router = headingRouter;
                }
                else
                {
                    break;
                }
            }

            foreach (var headingRouter in _headingRouters)
            {
                headingRouter.IsExpand = false;
            }

            if (router != null)
            {
                MainTreeView.SelectedItem = router;
                router.IsExpand = true;
                while (router.Parent != null)
                {
                    router.Parent.IsExpand = true;
                    router = router.Parent;
                }
            }
        }

        #endregion

        if (Math.Abs(scrollServiceCurrentScrollOffset.Y - _scrollViewerService.ScrollViewer.ScrollBarMaximum.Y) <
            App.MainView.BottomNavView.Bounds.Height)
        {
            return;
        }

        #region 设置位置可悬浮

        var locationY = locationPoint.Value - App.MainView.TopNavView.Bounds.Height + 20;
        // _logger.LogDebug("LocationY: " + locationY);
        if (locationY < 0)
        {
            Margin = new Thickness(0, 4 - locationY, 0, 0);
        }
        else
        {
            Margin = new Thickness(0, 4, 0, 0);
        }

        #endregion
    }

    protected override void OnUnloaded(RoutedEventArgs e)
    {
        _scrollViewerService.ScrollChanged -= HandleScrollChanged;
        base.OnUnloaded(e);
    }
}

public partial class RouterItem : ObservableObject
{
    public string? Text { get; set; }

    public int Level { get; set; }

    public Control Control { get; set; } = null!;

    public RouterItem? Parent { get; set; }

    public ObservableCollection<RouterItem> Items { get; set; } = new();

    [ObservableProperty] private bool _isExpand;
}