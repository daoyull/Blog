using System.Collections.ObjectModel;
using Blog.Lib.Models;
using Blog.Lib.Service;
using BlogBackManager.Abstracts;
using BlogBackManager.Helpers;
using Common.Avalonia.Abstracts;
using Common.Avalonia.Service;
using Common.Lib.Plugins;
using Common.Lib.Service;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BlogBackManager.Dialogs;

public partial class BlogAddDialog : UserComponent<BlogAddDialogModel>
{
    public BlogAddDialog()
    {
        InitializeComponent();
    }
}

public partial class BlogAddDialogModel : BaseFormDialogModel<BlogAddDto>, IRefresh
{
    private readonly IBlogService _blogService;
    private readonly IGlobalNotify _notify;
    private readonly ITagService _tagService;
    private readonly ICategoryService _categoryService;

    [ObservableProperty] private List<TagCacheVo>? _tags;
    [ObservableProperty] private List<CategoryCacheVo>? _categorys;
    public ObservableCollection<TagCacheVo> SelectedTags { get; } = new();

    public BlogAddDialogModel()
    {
        // Design
    }

    public BlogAddDialogModel(IBlogService blogService, IGlobalNotify notify, ITagService tagService,
        ICategoryService categoryService)
    {
        _blogService = blogService;
        _notify = notify;
        _tagService = tagService;
        _categoryService = categoryService;
        PluginBuilder.AddPlugin<RefreshPlugin>();
    }


    public async Task Refresh()
    {
        Tags = null;
        Categorys = null;
        // 加载标签和分类
        var tagResult = await _tagService.GetCacheListAsync();
        var categoryList = await _categoryService.GetCacheListAsync();
        tagResult.Handle(tags => Tags = tags);
        categoryList.Handle(cats => Categorys = cats);
    }

    protected override async Task HandleCommand()
    {
        Model.TagIds = SelectedTags.Select(it => it.Id).ToList();
        await _blogService.AddAsync(Model);
    }

    [RelayCommand]
    private async Task ShowMarkView(string text)
    {
        var markPreview = new MarkPreview();
        markPreview.Text = text;
        await DialogHelper.ShowDialogAsync(markPreview);
    }
}