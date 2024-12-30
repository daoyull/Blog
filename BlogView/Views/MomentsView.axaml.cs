using Avalonia.Controls;
using BlogView.ViewModels;
using Common.Lib.Ioc;


namespace BlogView.Views;

public partial class MomentsView : UserControl
{
    public MomentsViewModel ViewModel { get; } = Ioc.Resolve<MomentsViewModel>();

    public MomentsView()
    {
        InitializeComponent();
        DataContext = ViewModel;
        Loaded += async (sender, args) => { await ViewModel.Refresh(); };
    }
}