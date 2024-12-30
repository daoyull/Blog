using Blog.Lib.Models;
using Blog.Lib.Service;
using BlogView.Service;

using Common.Lib.Service;
using Common.Mvvm.Abstracts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BlogView.ViewModels;

public partial class TopNavViewModel : BaseViewModel, IRefresh
{
    private readonly ICategoryService _categoryService;
    private readonly PageService _pageService;
    [ObservableProperty] private IconShow _iconShow = new();
    [ObservableProperty] private List<CategoryCacheVo>? _source;
    [ObservableProperty] private int _alpha = 255;

    public TopNavViewModel(ICategoryService categoryService, PageService pageService)
    {
        _categoryService = categoryService;
        _pageService = pageService;
        pageService.ViewChanged += HandleViewChanged;
        
    }


    public async Task Refresh()
    {
        var categoryRes = await _categoryService.GetCacheListAsync();
        Source = null;
        categoryRes.Handle(cats => Source = cats);
    }

    [RelayCommand]
    private void RouterIndex()
    {
        if (_pageService.ViewType == ViewType.Index)
        {
            return;
        }

        _pageService.RouterIndex();
    }

    [RelayCommand]
    private void RouterCategory(CategoryCacheVo categoryCacheVo)
    {
        _pageService.RouterCategoryBlogList(categoryCacheVo);
        CloseFlyout?.Invoke();
    }

    [RelayCommand]
    private void RouterArchives()
    {
        if (_pageService.ViewType == ViewType.Archives)
        {
            return;
        }

        _pageService.RouterArchives();
    }

    [RelayCommand]
    private void RouterMoment()
    {
        if (_pageService.ViewType == ViewType.Moments)
        {
            return;
        }

        _pageService.RouterMoments();
    }

    [RelayCommand]
    private void RouterFriend()
    {
        if (_pageService.ViewType == ViewType.Friends)
        {
            return;
        }

        _pageService.RouterFriend();
    }

    [RelayCommand]
    private void RouterAboutMe()
    {
        if (_pageService.ViewType == ViewType.AboutMe)
        {
            return;
        }

        _pageService.RouterAboutMe();
    }


    public Action? CloseFlyout;

    private void HandleViewChanged(ViewType obj)
    {
        IconShow.ChangeSelect(Enum.GetName(obj)!);
    }
}

public partial class IconShow : ObservableObject
{
    [ObservableProperty] private bool _index;
    [ObservableProperty] private bool _categoryBlogListView;
    [ObservableProperty] private bool _archives;
    [ObservableProperty] private bool _moments;
    [ObservableProperty] private bool _friends;
    [ObservableProperty] private bool _aboutMe;


    public void ChangeSelect(string name)
    {
        foreach (var propertyInfo in GetType().GetProperties())
        {
            if (propertyInfo.Name == name)
            {
                propertyInfo.SetValue(this, true);
            }
            else
            {
                propertyInfo.SetValue(this, false);
            }
        }
    }
}