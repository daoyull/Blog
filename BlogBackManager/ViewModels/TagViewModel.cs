using Blog.Lib.Models;
using Blog.Lib.Service;
using BlogBackManager.Dialogs;
using Common.Avalonia.Service;
using Common.Lib.Plugins;
using Common.Lib.Service;
using Common.Mvvm.Abstracts;
using Common.Mvvm.Plugins;
using Common.Mvvm.Service;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mapster;
using Ursa.Controls;
using static BlogBackManager.Helpers.DialogHelper;

namespace BlogBackManager.ViewModels;

public partial class TagViewModel : BaseTableViewModel<TagVo>, IPagination<TagPageQueryDto>, IRefresh
{
    private readonly ITagService _tagService;
    private readonly IGlobalNotify _notify;

    public TagViewModel()
    {
        // Design
        Pagination.Total = 20;
        Pagination.PageNum = 1;
        Pagination.PageSize = 5;
        SetDataSource(new List<TagVo>()
        {
            new TagVo()
            {
                TagName = "Tag1",
                TagColor = "#B2F918"
            },
            new TagVo()
            {
                TagName = "Tag2",
                TagColor = "#FC2929"
            }
        });
    }

    public TagViewModel(ITagService tagService, IGlobalNotify notify)
    {
        Plugins.Add(new PaginationPlugin());
        Plugins.Add(new RefreshPlugin());

        _tagService = tagService;
        _notify = notify;
        PageChangeCommand = new AsyncRelayCommand<TagPageQueryDto>(_ => Refresh());

        // 初始化Command
        RefreshCommand = new AsyncRelayCommand(Refresh);
        ResetSearchBarCommand = new AsyncRelayCommand(ResetSearchBar);
        AddRowCommand = new AsyncRelayCommand(AddRow);
        EditRowCommand = new AsyncRelayCommand<TagVo>(EditRow);
        DeleteRowsCommand = new AsyncRelayCommand<List<TagVo>>(DeleteRows);
        DeleteRowCommand = new AsyncRelayCommand<TagVo>(DeleteRow);
    }

    private async Task DeleteRow(TagVo? arg)
    {
        if (arg == null)
        {
            return;
        }

        MessageBoxResult result =
            await MessageBox.ShowAsync($"确认删除标签名称为{arg.TagName}的数据吗?", "删除确认", MessageBoxIcon.Question);
        if (result != MessageBoxResult.OK)
        {
            return;
        }

        await _tagService.DeleteAsync(arg.Id!.Value);
        _notify.SuccessTip("删除成功");
        Pagination.PageNum = 1;
        await Refresh();
    }

    private async Task DeleteRows(List<TagVo>? arg)
    {
        if (arg == null || arg.Count == 0)
        {
            return;
        }

        MessageBoxResult result =
            await MessageBox.ShowAsync($"确认删除选中的{arg.Count}条的数据吗?", "删除确认", MessageBoxIcon.Question);
        if (result != MessageBoxResult.OK)
        {
            return;
        }

        await _tagService.DeleteListAsync(arg.Select(it => it.Id!.Value).ToList());
        _notify.SuccessTip("删除成功");
        Pagination.PageNum = 1;
        await Refresh();
    }

    private async Task EditRow(TagVo? arg)
    {
        await ShowDialogAsync<TagEditDialog, TagEditDialogModel>(new()
        {
            InitCommand = new AsyncRelayCommand<TagEditDialog>(async dialog =>
            {
                dialog!.ViewModel!.Model = arg.Adapt<TagEditDto>();
            }),
            OkCommand = new AsyncRelayCommand<TagEditDialog>(async dialog => { await Refresh(); })
        });
    }

    private async Task AddRow()
    {
        await ShowDialogAsync<TagAddDialog, TagAddDialogModel>(new()
        {
            OkCommand = new AsyncRelayCommand<TagAddDialog>(async dialog => { await Refresh(); })
        });
    }


    private async Task ResetSearchBar()
    {
        Pagination.PageNum = 1;
        Pagination.PageSize = 10;
        Pagination.TagName = string.Empty;
        await Refresh();
    }

    public async Task Refresh()
    {
        var result = await _tagService.GetPageAsync(Pagination);
        result.IfSucc(dataSource =>
        {
            Pagination.Total = dataSource.Total;
            IsPaginationShow = Pagination.Total > Pagination.PageSize;
            SetDataSource(dataSource.List);
        });
    }


    public TagPageQueryDto Pagination { get; } = new() { PageSize = 10 };

    [ObservableProperty] private bool _isPaginationShow;

    public AsyncRelayCommand<TagPageQueryDto> PageChangeCommand { get; set; }
}