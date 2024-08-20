using Blog.Lib.Models;
using Blog.Lib.Service;
using Common.Lib.Plugins;
using Common.Lib.Service;
using Common.Mvvm.Abstracts;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BlogView.ViewModels;

public partial class BlogDetailViewModel : BaseViewModel, IRefresh
{
    private readonly IBlogService _blogService;

    /// <summary>
    /// 当前需要展示的博客
    /// </summary>
    [ObservableProperty] private BlogCardVo? _model;

    public BlogDetailViewModel(IBlogService blogService)
    {
        _blogService = blogService;
        PluginBuilder.AddPlugin<RefreshPlugin>();
    }

    public long BlogId { get; set; }


    public async Task Refresh()
    {
        Model = null;
        var detailResult = await _blogService.GetBlogContentAsync(BlogId);
        detailResult.Handle(model => Model = model);
    }
}