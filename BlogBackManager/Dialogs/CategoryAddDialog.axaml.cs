using Avalonia.Controls;
using Blog.Lib.Models;
using Blog.Lib.Service;
using BlogBackManager.Abstracts;
using Common.Avalonia.Abstracts;

namespace BlogBackManager.Dialogs;

public partial class CategoryAddDialog : UserComponent<CategoryAddDialogModel>
{
    public CategoryAddDialog()
    {
        InitializeComponent();
    }
}

public partial class CategoryAddDialogModel : BaseFormDialogModel<CategoryAddDto>
{
    private readonly ICategoryService _categoryService;

    public CategoryAddDialogModel()
    {
    }

    public CategoryAddDialogModel(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }


    protected override async Task HandleCommand()
    {
        await _categoryService.AddAsync(Model);
    }
}