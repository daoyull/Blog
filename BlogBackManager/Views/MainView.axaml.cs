using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Notifications;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using BlogBackManager.ViewModels;
using Common.Avalonia.Abstracts;
using Common.Avalonia.Service;
using Common.Lib.Ioc;
using Common.Lib.Service;

namespace BlogBackManager.Views;

public partial class MainView : UserComponent<MainViewModel>
{
    public MainView()
    {
        InitializeComponent();
    }

    protected override void OnLoaded(RoutedEventArgs e)
    {
        base.OnLoaded(e);
        Ioc.Resolve<IGlobalNotify>().NotificationManager =
            new WindowNotificationManager(TopLevel.GetTopLevel(this));
    }

    private async void Button_OnClick(object? sender, RoutedEventArgs e)
    {
    }

    private void ClearContent(object? sender, RoutedEventArgs e)
    {
        ContentControl.Content = null;
    }

    private async void GcClick(object? sender, RoutedEventArgs e)
    {
        GC.Collect();
        GC.WaitForPendingFinalizers();
    }
}