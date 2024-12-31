using Blog.Lib.Models;
using Blog.Lib.Service;
using BlogView.Helpers;

using Common.Lib.Service;
using Common.Mvvm.Abstracts;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BlogView.ViewModels;

public partial class FriendViewModel : BaseViewModel, IRefresh
{
    private readonly IFriendService _friendService;

    [ObservableProperty] private List<FriendVo>? _source;

    [ObservableProperty] private string? _desc;

    public FriendViewModel(IFriendService friendService)
    {
        _friendService = friendService;
        
    }

    public async Task Refresh()
    {
        Source = null;
        var list = await _friendService.GetFriendVoList(10);
         list.ForEach(it => it.Color = RandomColorHelper.GetRandomColorStr());
        Source = list;


        var desc = await _friendService.GetDesc();
        Desc = desc.Content;
    }
}