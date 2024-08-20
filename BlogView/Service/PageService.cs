using Avalonia;
using Avalonia.Controls;
using Blog.Lib.Models;
using BlogView.ViewModels;
using BlogView.Views;
using CommunityToolkit.Mvvm.Input;

namespace BlogView.Service;

public class PageService
{
    private readonly MainViewModel _mainViewModel;
    private readonly ScrollViewerService _scrollViewerService;

    public PageService(MainViewModel mainViewModel, ScrollViewerService scrollViewerService)
    {
        _mainViewModel = mainViewModel;
        _scrollViewerService = scrollViewerService;
        RouterBlogDetailCommand = new RelayCommand<long>(RouterBlogDetail);
    }

    public ViewType ViewType { get; private set; }

    public Action<ViewType>? ViewChanged { get; set; }

    public void ChangeMainView(ViewType viewType, Control control)
    {
        _mainViewModel.MainView = control;
        ViewType = viewType;
        ViewChanged?.Invoke(viewType);
        _scrollViewerService.ChangeOffset(0);
    }

    public void RefreshView()
    {
    }

    public RelayCommand<long> RouterBlogDetailCommand { get; set; }

    public void RouterBlogDetail(long blogId)
    {
        var view = new BlogDetailView();
        view.ViewModel!.BlogId = blogId;
        ChangeMainView(ViewType.BlogDetail, view);
    }


    public void RouterTagBlogList(TagCacheVo tag)
    {
        var view = new BlogCardListView();
        view.ViewModel!.PageType = PageType.Tag;
        view.ViewModel.Id = tag.Id;
        ChangeMainView(ViewType.TagBlogListView, view);
    }

    public void RouterCategoryBlogList(CategoryCacheVo category)
    {
        var view = new BlogCardListView();

        view.ViewModel!.PageType = PageType.Category;
        view.ViewModel.Id = category.Id;

        ChangeMainView(ViewType.CategoryBlogListView, view);
    }


    public void RouterIndex()
    {
        var view = new BlogCardListView();
        view.ViewModel!.PageType = PageType.Index;
        ChangeMainView(ViewType.Index, view);
    }

    public void RouterArchives()
    {
        var view = new ArchiveView();
        ChangeMainView(ViewType.Archives, view);
    }

    public void RouterMoments()
    {
        var view = new MomentsView();
        ChangeMainView(ViewType.Moments, view);
    }

    public void RouterFriend()
    {
        var view = new FriendView();
        ChangeMainView(ViewType.Friends, view);
    }

    public void RouterAboutMe()
    {
        var view = new AboutMeView();
        ChangeMainView(ViewType.AboutMe, view);
    }
}

public enum ViewType
{
    Index,
    CategoryBlogListView,
    Archives,
    Moments,
    Friends,
    AboutMe,
    BlogDetail,
    TagBlogListView
}
