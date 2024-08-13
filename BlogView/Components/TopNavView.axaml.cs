using Avalonia.Controls;
using Avalonia.Interactivity;
using BlogView.ViewModels;
using Common.Avalonia.Abstracts;

namespace BlogView.Components;

public partial class TopNavView : UserComponent<TopNavViewModel>
{
    public TopNavView()
    {
        InitializeComponent();
    }


    protected override void OnLoaded(RoutedEventArgs e)
    {
        base.OnLoaded(e);
        if (ViewModel != null)
        {
            ViewModel.CloseFlyout += CloseFlyout;
        }
    }

    private void CloseFlyout()
    {
        CategoryButton.Flyout?.Hide();
    }

    protected override void OnUnloaded(RoutedEventArgs e)
    {
        base.OnUnloaded(e);
        if (ViewModel != null)
        {
            ViewModel.CloseFlyout -= CloseFlyout;
        }
    }
}