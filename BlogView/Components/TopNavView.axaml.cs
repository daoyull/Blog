using Avalonia.Controls;
using Avalonia.Interactivity;
using BlogView.ViewModels;
using Common.Lib.Ioc;


namespace BlogView.Components;

public partial class TopNavView : UserControl
{
    public TopNavView()
    {
        InitializeComponent();
        Loaded +=async (sender, args) =>
        {
            await ViewModel.Refresh();
        };
    }

    public TopNavViewModel ViewModel { get; } = Ioc.Resolve<TopNavViewModel>();

    protected override void OnLoaded(RoutedEventArgs e)
    {
        base.OnLoaded(e);
        ViewModel.CloseFlyout += CloseFlyout;
    }

    private void CloseFlyout()
    {
        CategoryButton.Flyout?.Hide();
    }

    protected override void OnUnloaded(RoutedEventArgs e)
    {
        base.OnUnloaded(e);
        ViewModel.CloseFlyout -= CloseFlyout;
    }
}