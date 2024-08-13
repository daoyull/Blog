using System.Globalization;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using BlogBackManager.Views;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;

namespace BlogBackManager;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
        
    }

    public static readonly MainView MainView = new();

    public override void OnFrameworkInitializationCompleted()
    {
        var mainView = new MainView();
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow()
            {
                Content = MainView
            };
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            singleViewPlatform.MainView = MainView;
        }

        base.OnFrameworkInitializationCompleted();
    }
}