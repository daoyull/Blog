using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Blog.Lib.Models;
using BlogBackManager.ViewModels;
using Common.Avalonia.Abstracts;

namespace BlogBackManager.Views;

public partial class BlogView : UserGridComponent<BlogViewModel, BlogVo>
{
    public BlogView()
    {
        InitializeComponent();
    }

    public override SelectedMode SelectedMode => SelectedMode.Multi;
    public override DataGrid? DataGrid => MainDataGrid;
}