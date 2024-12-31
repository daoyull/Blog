using Blog.Lib.Entity;
using Blog.Lib.Models;
using Blog.Lib.Service;
using BlogView.Service;
using Common.Lib.Ioc;

using Common.Lib.Service;
using Common.Mvvm.Abstracts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Markdig.Avalonia.Entity;

namespace BlogView.ViewModels;

public partial class RightNavViewModel : BaseViewModel, IRefresh
{
    private readonly ITagService _tagService;
    private readonly IBlogService _blogService;

    /// <summary>
    /// 标签列表
    /// </summary>
    [ObservableProperty] private List<TagCacheVo>? _tags;

    /// <summary>
    /// 随机博客列表
    /// </summary>
    [ObservableProperty] private List<RandomBlog>? _blogs;

    [ObservableProperty] private List<HeadingRouter> _routers = new();

    public RightNavViewModel(ITagService tagService, IBlogService blogService)
    {
        _tagService = tagService;
        _blogService = blogService;
        
    }

    public async Task Refresh()
    {
        Tags = null;
        Blogs = null;
        Tags = await _tagService.GetCacheListAsync();
        Blogs = await _blogService.QueryRandomBlogs(5);
    }

    [RelayCommand]
    private void RouterBlogDetail(long id)
    {
        Ioc.Resolve<PageService>().RouterBlogDetail(id);
    }

    [RelayCommand]
    private void RouterTag(TagCacheVo tag)
    {
        var viewService = Ioc.Resolve<PageService>();
        viewService.RouterTagBlogList(tag);
    }
}