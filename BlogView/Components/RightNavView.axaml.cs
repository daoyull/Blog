using Avalonia.Controls;
using BlogView.ViewModels;
using Common.Avalonia.Abstracts;

namespace BlogView.Components;

public partial class RightNavView : UserComponent<RightNavViewModel>
{
    public RightNavView()
    {
        InitializeComponent();
    }
}
