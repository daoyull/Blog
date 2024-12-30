using System.Collections.Generic;
using System.Threading.Tasks;
using Avalonia.Threading;
using Blog.Lib.Models;
using Blog.Lib.Service;
using Common.Lib.Exceptions;
using Common.Lib.Models;

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

    public BlogCardListViewModel(IBlogService blogService, ICategoryService categoryService, ITagService tagService)
    {
        _blogService = blogService;
        _categoryService = categoryService;
        _tagService = tagService;
        
    }

    public PageType PageType { get; set; }
    public long Id { get; set; }

    private void RefreshUi(string title, PageResult<BlogCardVo>? source)
    {
        if (source == null)
        {
            return;
        }

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
        await Refresh();
    }

    public async Task Refresh()
    {
        string title = string.Empty;
        PageResult<BlogCardVo> pageResult = default;

        switch (PageType)
        {
            case PageType.Index:
                var queryCardPageList = await _blogService.QueryCardPageList(new BlogPageQueryDto()
                {
                    PageNum = PageModel.PageNum,
                    PageSize = PageModel.PageSize
                });
                queryCardPageList.Handle(re =>
                {
                    pageResult = re;
                    RefreshUi(title, pageResult);
                });
                break;
            case PageType.Category:
                if (Id == 0)
                {
                    throw new BusinessException("分类不能为空");
                }


                var categoryResult = await _categoryService.GetAsync(new CategoryQueryDto() { Id = Id });
                categoryResult.Handle(async category =>
                {
                    title = $"分类{category.CategoryName}下的文章";
                    var catResult =
                        await _blogService.GetBlogPageByCategoryId(Id, PageModel.PageNum, PageModel.PageSize);
                    catResult.Handle(re =>
                    {
                        pageResult = re;
                        RefreshUi(title, pageResult);
                    });
                });
                break;
            case PageType.Tag:
                if (Id == 0)
                {
                    throw new BusinessException("标签不能为空");
                }

                var tagResult = await _tagService.GetAsync(new TagQueryDto() { Id = Id });
                tagResult.Handle(async tag =>
                {
                    title = $"标签{tag.TagName}下的文章";
                    var tagBlogs = await _blogService.GetBlogPageByTagId(Id, PageModel.PageNum, PageModel.PageSize);
                    tagBlogs.Handle(re =>
                    {
                        pageResult = re;
                        RefreshUi(title, pageResult);
                    });
                });
                break;
        }
    }
}

public enum PageType
{
    Index,
    Category,
    Tag
}