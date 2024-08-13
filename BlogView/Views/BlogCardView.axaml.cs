using BlogView.ViewModels;
using Common.Avalonia.Abstracts;

namespace BlogView.Views;

public partial class BlogCardListView : UserComponent<BlogCardListViewModel>
{
    public BlogCardListView()
    {
        InitializeComponent();
    }
}
