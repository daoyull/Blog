using System.Collections.Generic;
using System.Threading.Tasks;
using Avalonia.Threading;
using Blog.Lib.Models;
using Blog.Lib.Service;
using Common.Lib.Exceptions;
using Common.Lib.Models;
using Common.Lib.Plugins;
using Common.Lib.Service;
using Common.Mvvm.Abstracts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PageModel = AvaloniaBlog.Lib.Models.PageModel;

namespace BlogView.ViewModels;

public partial class BlogCardListViewModel : BaseViewModel, IRefresh
{
    private readonly IBlogService _blogService;
    private readonly ICategoryService _categoryService;
    private readonly ITagService _tagService;

    [ObservableProperty] private List<BlogCardVo>? _cardVos;

    [ObservableProperty] private string? _topTitle;

    [ObservableProperty] private PageModel _pageModel = new PageModel
    {
        PageNum = 1,
        PageSize = 5
    };


    [ObservableProperty] private bool _pageIsShow;

    private PageType _pageType = PageType.Index;
    private long _id = 0;

    public BlogCardListViewModel(IBlogService blogService, ICategoryService categoryService, ITagService tagService)
    {
        _blogService = blogService;
        _categoryService = categoryService;
        _tagService = tagService;
        PluginBuilder.AddPlugin<RefreshPlugin>();
    }

    public async Task RefreshPage(PageType pageType, long id = 0)
    {
        _pageType = pageType;
        string title = string.Empty;
        PageResult<BlogCardVo> result = default;

        switch (pageType)
        {
            case PageType.Index:
                _id = 0;
                var queryCardPageList = await _blogService.QueryCardPageList(new BlogPageQueryDto()
                {
                    PageNum = PageModel.PageNum,
                    PageSize = PageModel.PageSize
                });
                queryCardPageList.Handle(re =>
                {
                    result = re;
                    RefreshUi(title, result);
                });
                break;
            case PageType.Category:
                if (id == 0)
                {
                    throw new BusinessException("分类不能为空");
                }

                _id = id;

                var categoryResult = await _categoryService.GetAsync(new CategoryQueryDto() { Id = id });
                categoryResult.Handle(async category =>
                {
                    title = $"分类{category.CategoryName}下的文章";
                    var catResult =
                        await _blogService.GetBlogPageByCategoryId(id, PageModel.PageNum, PageModel.PageSize);
                    catResult.Handle(re =>
                    {
                        result = re;
                        RefreshUi(title, result);
                    });
                });
                break;
            case PageType.Tag:
                if (id == 0)
                {
                    throw new BusinessException("标签不能为空");
                }

                _id = id;
                var tagResult = await _tagService.GetAsync(new TagQueryDto() { Id = id });
                tagResult.Handle(async tag =>
                {
                    title = $"标签{tag.TagName}下的文章";
                    var tagBlogs = await _blogService.GetBlogPageByTagId(id, PageModel.PageNum, PageModel.PageSize);
                    tagBlogs.Handle(re =>
                    {
                        result = re;
                        RefreshUi(title, result);
                    });
                });
                break;
        }
    }

    private void RefreshUi(string title, PageResult<BlogCardVo>? source)
    {
        List<BlogCardVo> blogCardVos = source.List;
        Dispatcher.UIThread.Post(() =>
        {
            PageIsShow = source.Total > PageModel.PageSize;
            PageModel.Total = source.Total;
            TopTitle = title;
            CardVos = null;
            CardVos = blogCardVos;
        });
    }

    [RelayCommand]
    private async Task PageChanged(int pageNum)
    {
        PageModel.PageNum = pageNum;
        await RefreshPage(_pageType, _id);
    }

    public async Task Refresh()
    {
        await RefreshPage(_pageType, _id);
    }
}

public enum PageType
{
    Index,
    Category,
    Tag
}