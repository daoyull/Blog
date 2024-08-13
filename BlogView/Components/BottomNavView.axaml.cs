using Avalonia.Controls;
using BlogView.ViewModels;
using Common.Avalonia.Abstracts;

namespace BlogView.Components;

public partial class BottomNavView : UserComponent<BottomNavViewModel>
{
    public BottomNavView()
    {
        InitializeComponent();
    }
}
