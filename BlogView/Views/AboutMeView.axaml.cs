using Avalonia.Controls;
using BlogView.ViewModels;
using Common.Lib.Ioc;


namespace BlogView.Views;

public partial class AboutMeView : UserControl
{
    public AboutMeViewModel ViewModel { get; } = Ioc.Resolve<AboutMeViewModel>();
    public AboutMeView()
    {
        InitializeComponent();
        DataContext = ViewModel;
        Loaded += async (sender, args) =>
        {
            await ViewModel.Refresh();
        };
    }
}