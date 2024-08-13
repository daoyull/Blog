using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Avalonia;
using Blog.DbModule;
using Common.Avalonia.Service;
using Common.FreeSql;
using Common.FreeSql.Models;
using Common.Lib.Exceptions;
using Common.Lib.Ioc;
using Common.Redis;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BlogBackManager.Desktop.Db;

sealed class Program
{
    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    [STAThread]
    public static void Main(string[] args)
    {
        var hostBuilder = Host.CreateDefaultBuilder(args);
#if DEBUG
        hostBuilder.UseEnvironment("Development");
#else
        hostBuilder.UseEnvironment("Production");
#endif

        try
        {
            using IHost host = hostBuilder.ConfigureServices(CreateDefaultServices)
                .Build();


            host.Start();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        try
        {
            BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
        }
        catch (Exception e)
        {
            try
            {
                var notify = Ioc.ResolveOptional<IGlobalNotify>();
                if (e is BusinessException businessException)
                {
                    notify?.Error("错误", businessException.Message);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }
    }

    private static void CreateDefaultServices(HostBuilderContext host, IServiceCollection service)
    {

        #region Config

        service.AddOptions<FreeSqlOptions>("blog").BindConfiguration("blog_db");
        service.AddOptions<RedisOptions>("blog").BindConfiguration("blog_redis");

        #endregion
        
        #region AutoFac

        Ioc.Register(builder =>
        {
            builder.RegisterModule<BlogDbModule>();
            builder.RegisterModule<BlogBackManagerModule>();
            builder.AddFreeSql();
            builder.Populate(service);
        });
        Ioc.Builder();
        

        #endregion
    }

    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
    {
        var appBuilder = AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace();

        return appBuilder;
    }
}