using Avalonia.Controls;
using Avalonia.Interactivity;
using BlogView.Components;
using BlogView.ViewModels;
using Common.Lib.Ioc;
using Common.Lib.Service;

namespace BlogView.Views;

public partial class BlogDetailView : UserControl
{
    readonly HeadRoutingView _headRoutingView = new HeadRoutingView();
    public BlogDetailViewModel ViewModel { get; } = Ioc.Resolve<BlogDetailViewModel>();
    public BlogDetailView()
    {
        InitializeComponent();
        DataContext = ViewModel;
        Unloaded += HandleUnload;
        App.MainView.RightNavView.MainStackPanel.Children.Add(_headRoutingView.LocationControl);
        App.MainView.RightNavView.MainStackPanel.Children.Add(_headRoutingView);
        MarkView.HeadingLoaded += _headRoutingView.HandleMarkViewLoaded;
        Loaded +=async (sender, args) =>
        {
            await ViewModel.Refresh();
        };
    }

    private void HandleUnload(object? sender, RoutedEventArgs e)
    {
        App.MainView.RightNavView.MainStackPanel.Children.Remove(_headRoutingView.LocationControl);
        App.MainView.RightNavView.MainStackPanel.Children.Remove(_headRoutingView);
        MarkView.HeadingLoaded -= _headRoutingView.HandleMarkViewLoaded;
    }
}