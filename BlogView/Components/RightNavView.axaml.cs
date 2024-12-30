using Avalonia.Controls;
using BlogView.ViewModels;
using Common.Lib.Ioc;


namespace BlogView.Components;

public partial class RightNavView : UserControl
{
    public RightNavViewModel ViewModel { get; } = Ioc.Resolve<RightNavViewModel>();

    public RightNavView()
    {
        InitializeComponent();
        DataContext = ViewModel;
        Loaded +=async (sender, args) =>
        {
            await ViewModel.Refresh();
        };
    }
}