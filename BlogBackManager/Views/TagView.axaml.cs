using Avalonia.Controls;
using Blog.Lib.Models;
using BlogBackManager.ViewModels;
using Common.Avalonia.Abstracts;

namespace BlogBackManager.Views;

public partial class TagView : UserGridComponent<TagViewModel, TagVo>
{
    public TagView()
    {
        InitializeComponent();
    }

    public override SelectedMode SelectedMode => SelectedMode.Multi;

    public override DataGrid DataGrid => MainDataGrid;
}