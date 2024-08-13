using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Blog.Lib.Models;
using BlogBackManager.ViewModels;
using Common.Avalonia.Abstracts;

namespace BlogBackManager.Views;

public partial class CategoryView : UserGridComponent<CategoryViewModel, CategoryVo>
{
    public CategoryView()
    {
        InitializeComponent();
    }

    public override SelectedMode SelectedMode { get; } = SelectedMode.Multi;
    public override DataGrid? DataGrid => MainDataGrid;
}