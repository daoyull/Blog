using AvaloniaBlog.Lib.Controls;
using Markdig.Avalonia;
using Markdig.Avalonia.Avalonia.Inline;
using Markdig.Avalonia.Controls;
using Markdig.Syntax.Inlines;

namespace AvaloniaBlog.Lib.CustomerRender;

public class CusLinkInlineRenderer : LinkInlineRenderer
{
    protected override ImageAsync CreateImageAsync()
    {
        return new CacheImageAsync();
    }
}