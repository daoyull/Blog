using System.Threading.Tasks;
using Blog.Lib.Models;
using Blog.Lib.Service;
using Common.Lib.Service;
using Common.Mvvm.Abstracts;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BlogView.ViewModels;

public partial class BlogDetailViewModel : BaseViewModel
{
    private readonly IBlogService _blogService;

    /// <summary>
    /// 当前需要展示的博客
    /// </summary>
    [ObservableProperty] private BlogCardVo? _model;

    public BlogDetailViewModel(IBlogService blogService)
    {
        _blogService = blogService;
    }

    public async Task LoadBlogDetail(long blogId)
    {
        Model = null;
        var detailResult = await _blogService.GetBlogContentAsync(blogId);
        detailResult.Handle(model => Model = model);
    }
    
}