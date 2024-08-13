using Autofac;
using Blog.DbModule.Service;
using Blog.DbModule.Service.Impl;
using Blog.Lib.Service;
using Common.Lib.Abstracts;

namespace Blog.DbModule;

public class BlogDbModule : BaseModule
{
    public const string BlogDatabaseName = "blog";

    public override void LoadService(ContainerBuilder builder)
    {
        builder.RegisterType<TagServiceImpl>().As<ITagService>().SingleInstance();
        builder.RegisterType<InformationServiceImpl>().As<IInformationService>().SingleInstance();
        builder.RegisterType<ConfigServiceImpl>().As<IConfigService>().SingleInstance();
        builder.RegisterType<BlogServiceImpl>().As<IBlogService>().SingleInstance();
        builder.RegisterType<CategoryServiceImpl>().As<ICategoryService>().SingleInstance();
        builder.RegisterType<AboutMeServiceImpl>().As<IAboutMeService>().SingleInstance();
        builder.RegisterType<FriendServiceImpl>().As<IFriendService>().SingleInstance();
        builder.RegisterType<MomentServiceImpl>().As<IMomentService>().SingleInstance();
        builder.RegisterType<UserServiceImpl>().As<IUserService>().SingleInstance();
        builder.RegisterType<JwtServiceImpl>().As<IJwtService>().SingleInstance();
        builder.RegisterType<SiteInfoServiceImpl>().As<ISiteService>().SingleInstance();
        builder.RegisterType<VisitLogServiceImpl>().As<IVisitLogService>().SingleInstance();
    }
}