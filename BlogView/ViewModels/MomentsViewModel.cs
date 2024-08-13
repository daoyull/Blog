using AvaloniaBlog.Lib.Models;
using Blog.Lib.Models;
using Blog.Lib.Service;
using Common.Lib.Plugins;
using Common.Lib.Service;
using Common.Mvvm.Abstracts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mapster;

namespace BlogView.ViewModels;

public partial class MomentsViewModel : BaseViewModel, IRefresh
{
    private readonly IMomentService _momentService;
    [ObservableProperty] private List<MomentVo>? _source;

    [ObservableProperty] private PageModel _pageModel = new PageModel
    {
        PageNum = 1,
        PageSize = 5
    };

    [ObservableProperty] private bool _pageIsShow;

    public MomentsViewModel(IMomentService momentService)
    {
        _momentService = momentService;
        Plugins.Add(new RefreshPlugin());
    }

    [RelayCommand]
    private async Task PageChanged(int pageNum)
    {
        PageModel.PageNum = pageNum;
        await Refresh();
    }

    public async Task Refresh()
    {
        Source = null;
        var queryDto = PageModel.Adapt<MomentPageQueryDto>();
        var listFrontResult = await _momentService.GetMomentPageList(queryDto);
        listFrontResult.Handle(page =>
        {
            PageIsShow = page.Total > PageModel.PageSize;

            Source = page.List;
            PageModel.Total = page.Total;
        });
    }
}