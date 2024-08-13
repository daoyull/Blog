using BlogView.ViewModels;
using Common.Avalonia.Abstracts;

namespace BlogView.Views;

public partial class AboutMeView : UserComponent<AboutMeViewModel>
{
    public AboutMeView()
    {
        InitializeComponent();
    }
}
