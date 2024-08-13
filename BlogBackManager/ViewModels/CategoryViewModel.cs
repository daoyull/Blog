using Blog.Lib.Models;
using Blog.Lib.Service;
using BlogBackManager.Dialogs;
using BlogBackManager.Helpers;
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

namespace BlogBackManager.ViewModels;

public partial class CategoryViewModel : BaseTableViewModel<CategoryVo>, IPagination<CategoryPageQueryDto>, IRefresh
{
    private readonly ICategoryService _categoryService;
    private readonly IGlobalNotify _notify;

    public CategoryViewModel(ICategoryService categoryService, IGlobalNotify notify)
    {
        Plugins.Add(new PaginationPlugin());
        Plugins.Add(new RefreshPlugin());

        _categoryService = categoryService;
        _notify = notify;
        PageChangeCommand = new AsyncRelayCommand<CategoryPageQueryDto>(_ => Refresh());

        // 初始化Command
        RefreshCommand = new AsyncRelayCommand(Refresh);
        ResetSearchBarCommand = new AsyncRelayCommand(ResetSearchBar);
        AddRowCommand = new AsyncRelayCommand(AddRow);
        EditRowCommand = new AsyncRelayCommand<CategoryVo>(EditRow);
        DeleteRowsCommand = new AsyncRelayCommand<List<CategoryVo>>(DeleteRows);
        DeleteRowCommand = new AsyncRelayCommand<CategoryVo>(DeleteRow);
    }


    private async Task EditRow(CategoryVo? arg)
    {
        await DialogHelper.ShowDialogAsync<CategoryEditDialog, CategoryEditDialogModel>(new()
        {
            InitCommand = new AsyncRelayCommand<CategoryEditDialog>(async dialog =>
            {
                dialog!.ViewModel!.Model = arg.Adapt<CategoryEditDto>();
            }),
            OkCommand = new AsyncRelayCommand<CategoryEditDialog>(async dialog => { await Refresh(); })
        });
    }

    private async Task DeleteRow(CategoryVo? arg)
    {
        if (arg == null)
        {
            return;
        }

        MessageBoxResult result =
            await MessageBox.ShowAsync($"确认删除标签名称为{arg.CategoryName}的数据吗?", "删除确认", MessageBoxIcon.Question);
        if (result != MessageBoxResult.OK)
        {
            return;
        }

        await _categoryService.DeleteAsync(arg.Id!.Value);
        _notify.SuccessTip("删除成功");
        Pagination.PageNum = 1;
        await Refresh();
    }

    private async Task DeleteRows(List<CategoryVo>? arg)
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

        await _categoryService.DeleteListAsync(arg.Select(it => it.Id!.Value).ToList());
        _notify.SuccessTip("删除成功");
        Pagination.PageNum = 1;
        await Refresh();
    }

    private async Task AddRow()
    {
        await DialogHelper.ShowDialogAsync<CategoryAddDialog, CategoryAddDialogModel>(new()
        {
            OkCommand = new AsyncRelayCommand<CategoryAddDialog>(async dialog => { await Refresh(); })
        });
    }

    private async Task ResetSearchBar()
    {
        Pagination.PageNum = 1;
        Pagination.PageSize = 10;
        Pagination.CategoryName = string.Empty;
        await Refresh();
    }

    public async Task Refresh()
    {
        var result = await _categoryService.GetPageAsync(Pagination);
        result.Handle(dataSource =>
        {
            Pagination.Total = dataSource.Total;
            IsPaginationShow = Pagination.Total > Pagination.PageSize;
            SetDataSource(dataSource.List);
        });
    }

    public CategoryPageQueryDto Pagination { get; } = new();

    [ObservableProperty] private bool _isPaginationShow;


    public AsyncRelayCommand<CategoryPageQueryDto> PageChangeCommand { get; set; }
}