using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using BlogView.Service;
using BlogView.ViewModels;
using Common.Avalonia.Abstracts;
using Common.Lib.Ioc;

namespace BlogView.Views;

public partial class MainView : UserComponent<MainViewModel>
{
    public MainView()
    {
        InitializeComponent();
#if DEBUG
        TestButton.IsVisible = true;
#else
        TestButton.IsVisible = false;
#endif
    }


    protected override void OnLoaded(RoutedEventArgs e)
    {
        base.OnLoaded(e);
        try
        {
            var scrollViewer = Ioc.Resolve<ScrollViewerService>();
            scrollViewer.ScrollViewer = ScrollViewer;
            scrollViewer.Start();
            Ioc.Resolve<PageService>().RouterIndex();
        }
        catch (Exception ex)
        {
            // ignore
        }
    }
    

    private void TestButton_OnClick(object? sender, RoutedEventArgs e)
    {
    }
}