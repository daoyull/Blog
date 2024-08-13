using Blog.Lib.Models;
using Blog.Lib.Service;
using BlogBackManager.Abstracts;
using Common.Avalonia.Abstracts;
using Irihi.Avalonia.Shared.Contracts;
using Ursa.Controls;

namespace BlogBackManager.Dialogs;

public partial class TagEditDialog : UserComponent<TagEditDialogModel>
{
    public TagEditDialog()
    {
        InitializeComponent();
    }
}

public partial class TagEditDialogModel : BaseFormDialogModel<TagEditDto>
{
    private readonly ITagService _tagService;

    public TagEditDialogModel()
    {
        // Design
    }

    public TagEditDialogModel(ITagService tagService)
    {
        _tagService = tagService;
    }


    protected override async Task HandleCommand()
    {
        await _tagService.EditAsync(Model);
    }
}