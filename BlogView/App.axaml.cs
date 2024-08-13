using Autofac;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using AvaloniaBlog.Lib.CustomerRender;
using BlogView.Views;
using Common.Lib.Ioc;
using Markdig.Avalonia;
using Markdig.Avalonia.Avalonia;
using Markdig.Avalonia.Avalonia.Inline;
using Markdig.Renderers;


namespace BlogView;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public static readonly MainView MainView = new();

    public override void OnFrameworkInitializationCompleted()
    {
        // 
        AvaloniaRenderer.SetRendererCollectionAction(CustomRenderer);
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

    private void CustomRenderer(ObjectRendererCollection objectRenderer)
    {
        objectRenderer.Add(new ListRenderer());
        objectRenderer.Add(new HeadingRenderer());
        objectRenderer.Add(new ParagraphRenderer());
        objectRenderer.Add(new FencedCodeRenderer());
        // objectRenderer.Add(new MathBlock());
        // objectRenderer.Add(new YamlFrontMatterBlock());
        objectRenderer.Add(new QuoteBlockRenderer());
        objectRenderer.Add(new ThematicBreakRenderer());
        objectRenderer.Add(new HtmlBlockRenderer());

        // Default inline renderers
        objectRenderer.Add(new AutolinkInlineRenderer());
        objectRenderer.Add(new CodeInlineRenderer());
        objectRenderer.Add(new EmphasisInlineRenderer());
        // ObjectRenderers.Add(new HtmlEntityInlineRenderer());
        objectRenderer.Add(new CusLinkInlineRenderer());
        objectRenderer.Add(new LiteralInlineRenderer());

        // Extension renderers
        objectRenderer.Add(new TableRenderer());
        // ObjectRenderers.Add(new TaskListRenderer());
    }
}