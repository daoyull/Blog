using Avalonia.Controls;
using BlogView.ViewModels;
using Common.Lib.Ioc;


namespace BlogView.Components;

public partial class BottomNavView : UserControl
{
    public BottomNavViewModel ViewModel { get; } = Ioc.Resolve<BottomNavViewModel>();
    public BottomNavView()
    {
        InitializeComponent();
        DataContext = ViewModel;
        Loaded +=async (sender, args) =>
        {
            await ViewModel.Refresh();
        };
    }
}
