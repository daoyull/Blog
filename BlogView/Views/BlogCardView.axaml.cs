using Avalonia.Controls;
using BlogView.ViewModels;
using Common.Lib.Ioc;


namespace BlogView.Views;

public partial class BlogCardListView : UserControl
{
    public BlogCardListViewModel ViewModel { get; } = Ioc.Resolve<BlogCardListViewModel>();
    public BlogCardListView()
    {
        InitializeComponent();
        DataContext = ViewModel;
        Loaded +=async (sender, args) =>
        {
            await ViewModel.Refresh();
        };
    }
}
