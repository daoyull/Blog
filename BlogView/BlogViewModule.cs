using Autofac;
using BlogView.Components;
using BlogView.Service;
using BlogView.ViewModels;
using BlogView.Views;
using Common.Lib.Abstracts;


namespace BlogView;

public class BlogViewModule : BaseModule
{
    public override void LoadService(ContainerBuilder builder)
    {
        #region Page | ViewModel

        // 主页
        builder.RegisterType<MainViewModel>().SingleInstance();
        // 顶部菜单
        builder.RegisterType<TopNavViewModel>().SingleInstance();
        // 左侧页签
        builder.RegisterType<LeftNavViewModel>().SingleInstance();
        // 右侧页签
        builder.RegisterType<RightNavViewModel>().SingleInstance();
        // 底部页签
        builder.RegisterType<BottomNavViewModel>().SingleInstance();
        // 文章列表
        builder.RegisterType<BlogCardListViewModel>();
        // 归档
        builder.RegisterType<ArchiveViewModel>();
        // 文章详细信息
        builder.RegisterType<BlogDetailViewModel>();
        // 友链
        builder.RegisterType<FriendViewModel>();
        // 动态
        builder.RegisterType<MomentsViewModel>();
        // 关于我
        builder.RegisterType<AboutMeViewModel>();

        #endregion

        // Service
        builder.RegisterType<PageService>().SingleInstance();
        builder.RegisterType<ScrollViewerService>().SingleInstance();
    }
}