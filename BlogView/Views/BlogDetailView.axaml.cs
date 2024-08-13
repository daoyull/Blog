using BlogView.Components;
using BlogView.ViewModels;
using Common.Avalonia.Abstracts;
using Common.Lib.Service;

namespace BlogView.Views;

public partial class BlogDetailView : UserComponent<BlogDetailViewModel>
{
    public BlogDetailView()
    {
        InitializeComponent();
        Plugins.Add(new BlogDetailPlugin());
    }
}

public class BlogDetailPlugin : ILifePlugin
{
    readonly HeadRoutingView _headRoutingView = new HeadRoutingView();

    public Task OnCreate(ILifeCycle lifeCycle)
    {
        return Task.CompletedTask;
    }

    public Task OnInit(ILifeCycle lifeCycle)
    {
        if (lifeCycle is not BlogDetailView view)
        {
            return Task.CompletedTask;
        }

        App.MainView.RightNavView.MainStackPanel.Children.Add(_headRoutingView.LocationControl);
        App.MainView.RightNavView.MainStackPanel.Children.Add(_headRoutingView);
        view.MarkView.HeadingLoaded += _headRoutingView.HandleMarkViewLoaded;
        return Task.CompletedTask;
    }

    public Task OnLoad(ILifeCycle lifeCycle)
    {
        return Task.CompletedTask;
    }

    public Task OnUnload(ILifeCycle lifeCycle)
    {
        if (lifeCycle is not BlogDetailView view)
        {
            return Task.CompletedTask;
        }

        App.MainView.RightNavView.MainStackPanel.Children.Remove(_headRoutingView.LocationControl);
        App.MainView.RightNavView.MainStackPanel.Children.Remove(_headRoutingView);
        view.MarkView.HeadingLoaded -= _headRoutingView.HandleMarkViewLoaded;
        return Task.CompletedTask;
    }
}