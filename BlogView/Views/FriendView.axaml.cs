using BlogView.ViewModels;
using Common.Avalonia.Abstracts;

namespace BlogView.Views;

public partial class FriendView : UserComponent<FriendViewModel>
{
    public FriendView()
    {
        InitializeComponent();
    }
}
