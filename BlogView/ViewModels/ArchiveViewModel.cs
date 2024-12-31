using System.Collections.ObjectModel;
using Blog.Lib.Entity;
using Blog.Lib.Service;
using BlogView.Helpers;
using BlogView.Service;
using Common.Lib.Ioc;

using Common.Lib.Service;
using Common.Mvvm.Abstracts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BlogView.ViewModels;

public partial class ArchiveViewModel : BaseViewModel, IRefresh
{
    private readonly IBlogService _blogService;
    [ObservableProperty] private List<Archive>? _archives;
    [ObservableProperty] private string? _subTitle = "目前共计N篇文章。继续努力";

    public ArchiveViewModel(IBlogService blogService)
    {
        _blogService = blogService;
       
    }

    [RelayCommand]
    private void RouterBlogDetail(long id)
    {
        Ioc.Resolve<PageService>().RouterBlogDetail(id);
    }

    public async Task Refresh()
    {
        Archives = null;
        var list = await _blogService.QueryArchiveList();
        foreach (var archive in list)
        {
            archive.Color = RandomColorHelper.GetRandomColorStr();
        }

        SubTitle = $"目前共计{list.SelectMany(it => it.Blogs).Count()}篇文章。继续努力";
        list.Add(new Archive
        {
            Time = "Hello World",
            Color = "#000000",
        });

        Archives = list;
    }
}