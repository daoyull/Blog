using Avalonia.Controls;
using BlogView.ViewModels;
using Common.Lib.Ioc;


namespace BlogView.Views;

public partial class FriendView :UserControl
{
    public FriendViewModel ViewModel { get; } = Ioc.Resolve<FriendViewModel>();
    public FriendView()
    {
        InitializeComponent();
        DataContext = ViewModel;
        Loaded +=async (sender, args) =>
        {
            await ViewModel.Refresh();
        };
    }
}
