using System;
using System.IO;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Avalonia;
using Blog.DbModule;
using Common.FreeSql;
using Common.FreeSql.Models;
using Common.Lib.Ioc;
using Common.Redis;
using Markdig.Avalonia.Helper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TextMateSharp.Grammars;


namespace BlogView.Desktop.Db;

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
        
        using IHost host = hostBuilder.ConfigureServices(CreateDefaultServices)
            .Build();
        
        host.Start();
        BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
    }

    private static void CreateDefaultServices(HostBuilderContext host, IServiceCollection service)
    {
        // #region Log
        //
        // service.AddSerilog(configuration =>
        // {
        //     configuration.ReadFrom.Configuration(new ConfigurationBuilder()
        //         .SetBasePath(Directory.GetCurrentDirectory())
        //         .AddJsonFile($"appsettings.{host.HostingEnvironment.EnvironmentName}.json")
        //         .Build());
        // });
        // service.AddSingleton(Log.Logger);
        //
        // #endregion

        #region Config

        service.AddOptions<FreeSqlOptions>("blog").BindConfiguration("blog_db");
        service.AddOptions<RedisOptions>("blog").BindConfiguration("blog_redis");

        #endregion


        #region AutoFac

        Ioc.Register(builder =>
        {
            builder.RegisterModule<BlogDbModule>();
            builder.RegisterModule<BlogViewModule>();
            builder.AddFreeSql();
            builder.AddRedis();
            builder.Populate(service);
        });
        Ioc.Builder();

        #endregion

        Task.Run(() =>
        {
            CodeThemeManager.AddTheme(ThemeName.DarkPlus);
        });
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
