using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using BlogBackManager.ViewModels;
using Common.Avalonia.Abstracts;

namespace BlogBackManager.Views;

public partial class IndexView : UserComponent<IndexViewModel>
{
    public IndexView()
    {
        InitializeComponent();
    }
    
}