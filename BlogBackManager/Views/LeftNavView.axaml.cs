using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using BlogBackManager.ViewModels;
using Common.Avalonia.Abstracts;

namespace BlogBackManager.Views;

public partial class LeftNavView : UserComponent<LeftNavViewModel>
{
    public LeftNavView()
    {
        InitializeComponent();
    }
}