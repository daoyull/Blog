using Blog.Lib.Models;
using Blog.Lib.Service;
using BlogBackManager.Abstracts;
using Common.Avalonia.Abstracts;
using Irihi.Avalonia.Shared.Contracts;
using Ursa.Controls;

namespace BlogBackManager.Dialogs;

public partial class TagAddDialog : UserComponent<TagAddDialogModel>
{
    public TagAddDialog()
    {
        InitializeComponent();
    }
}

public partial class TagAddDialogModel : BaseFormDialogModel<TagAddDto>
{
    private readonly ITagService _tagService;

    public TagAddDialogModel()
    {
        // Design
    }

    public TagAddDialogModel(ITagService tagService)
    {
        _tagService = tagService;
    }


    protected override async Task HandleCommand()
    {
        await _tagService.AddAsync(Model);
    }
}