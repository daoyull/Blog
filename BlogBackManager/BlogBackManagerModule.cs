using Autofac;
using BlogBackManager.Dialogs;
using BlogBackManager.Service.Impl;
using BlogBackManager.ViewModels;
using Common.Avalonia.Service;
using Common.Lib.Abstracts;

namespace BlogBackManager;

public class BlogBackManagerModule : BaseModule
{
    public override void LoadService(ContainerBuilder builder)
    {
        builder.RegisterType<GlobalNotify>().As<IGlobalNotify>().SingleInstance();

        #region ViewModel

        builder.RegisterType<IndexViewModel>();
        
        builder.RegisterType<MainViewModel>().SingleInstance();
        builder.RegisterType<LeftNavViewModel>().SingleInstance();

        builder.RegisterType<TagViewModel>();
        builder.RegisterType<TagAddDialogModel>();
        builder.RegisterType<TagEditDialogModel>();

        builder.RegisterType<CategoryViewModel>();
        builder.RegisterType<CategoryAddDialogModel>();
        builder.RegisterType<CategoryEditDialogModel>();

        builder.RegisterType<BlogViewModel>();
        builder.RegisterType<BlogAddDialogModel>();
        builder.RegisterType<BlogEditDialogModel>();

        #endregion
    }
}