using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Blog.Lib.Models;
using Blog.Lib.Service;
using BlogBackManager.Abstracts;
using Common.Avalonia.Abstracts;

namespace BlogBackManager.Dialogs;

public partial class CategoryEditDialog : UserComponent<CategoryEditDialogModel>
{
    public CategoryEditDialog()
    {
        InitializeComponent();
    }
}

public partial class CategoryEditDialogModel : BaseFormDialogModel<CategoryEditDto>
{
    public CategoryEditDialogModel()
    {
    }

    public CategoryEditDialogModel(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    private readonly ICategoryService _categoryService;

    protected override async Task HandleCommand()
    {
        await _categoryService.EditAsync(Model!);
    }
}