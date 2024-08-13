using Avalonia.Markup.Xaml;
using Avalonia.Styling;

namespace AvaloniaBlog.Lib;

public class BlogTheme : Styles
{
    private readonly IServiceProvider? sp;

    public BlogTheme(IServiceProvider? provider = null)
    {
        sp = provider;
        AvaloniaXamlLoader.Load(provider, this);
    }
}