using Blog.Lib.Models;
using Blog.Lib.Service;
using BlogBackManager.Dialogs;
using BlogBackManager.Helpers;
using Common.Avalonia.Service;
using Common.Lib.Plugins;
using Common.Lib.Service;
using Common.Mvvm.Abstracts;
using Common.Mvvm.Service;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mapster;
using Ursa.Controls;

namespace BlogBackManager.ViewModels;

public partial class BlogViewModel : BaseTableViewModel<BlogVo>, IPagination<BlogPageQueryDto>, IRefresh
{
    private readonly IBlogService _blogService;
    private readonly IGlobalNotify _notify;

    public BlogViewModel()
    {
        // Design
    }


    public BlogViewModel(IBlogService blogService, IGlobalNotify notify)
    {
        _blogService = blogService;
        _notify = notify;
        AddRowCommand = new AsyncRelayCommand(AddRow);
        EditRowCommand = new AsyncRelayCommand<BlogVo>(EditRow);
        DeleteRowCommand = new AsyncRelayCommand<BlogVo>(DeleteRow);
        DeleteRowsCommand = new AsyncRelayCommand<List<BlogVo>>(DeleteRows);
        RefreshCommand = new AsyncRelayCommand(Refresh);
        ResetSearchBarCommand = new AsyncRelayCommand(ResetSearchBar);
        PluginBuilder.AddPlugin<RefreshPlugin>();
    }

    private async Task DeleteRows(List<BlogVo>? arg)
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

        await _blogService.DeleteListAsync(arg.Select(it => it.Id!.Value).ToList());
        _notify.SuccessTip("删除成功");
        Pagination.PageNum = 1;
        await Refresh();
    }

    private async Task DeleteRow(BlogVo? arg)
    {
        if (arg == null)
        {
            return;
        }

        MessageBoxResult result =
            await MessageBox.ShowAsync($"确认删除名称为{arg.Title}的数据吗?", "删除确认", MessageBoxIcon.Question);
        if (result != MessageBoxResult.OK)
        {
            return;
        }

        await _blogService.DeleteAsync(arg.Id!.Value);
        _notify.SuccessTip("删除成功");
        Pagination.PageNum = 1;
        await Refresh();
    }

    private async Task EditRow(BlogVo? arg)
    {
        await DialogHelper.ShowDialogAsync<BlogEditDialog, BlogEditDialogModel>(new()
        {
            InitCommand = new AsyncRelayCommand<BlogEditDialog>(async dialog =>
            {
                dialog!.ViewModel!.Model = arg.Adapt<BlogEditDto>();
            }),
            OkCommand = new AsyncRelayCommand<BlogEditDialog>(async dialog => { await Refresh(); })
        });
    }

    private async Task AddRow()
    {
        await DialogHelper.ShowDialogAsync<BlogAddDialog, BlogAddDialogModel>(new()
        {
            OkCommand = new AsyncRelayCommand<BlogAddDialog>(async dialog => { await Refresh(); })
        });
    }


    private async Task ResetSearchBar()
    {
        Pagination.PageNum = 1;
        Pagination.PageSize = 10;
        Pagination.Title = string.Empty;
        await Refresh();
    }

    public async Task Refresh()
    {
        var pageResult = await _blogService.GetPageAsync(Pagination);
        pageResult.Handle(page =>
        {
            Pagination.Total = page.Total;
            SetDataSource(page.List);
        });
    }


    public BlogPageQueryDto Pagination { get; } = new()
    {
        PageNum = 1,
        PageSize = 10,
        Title = string.Empty
    };

    [ObservableProperty] private bool _isPaginationShow;
    public AsyncRelayCommand<BlogPageQueryDto> PageChangeCommand { get; set; }
}