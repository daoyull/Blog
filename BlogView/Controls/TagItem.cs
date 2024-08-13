using System.Windows.Input;
using Autofac;
using Avalonia;
using Avalonia.Controls.Primitives;
using Blog.Lib.Models;
using BlogView.Service;
using Common.Lib.Ioc;
using CommunityToolkit.Mvvm.Input;

namespace BlogView.Controls;

public class TagItem : TemplatedControl
{
    static TagItem()
    {
    }

    public TagItem()
    {
        Command = new RelayCommand(async () =>
        {
            var viewService = Ioc.Resolve<PageService>();
            viewService.RouterTagBlogList(new TagCacheVo()
            {
                Id = Id,
                TagName = TagName,
                TagColor = TagColor,
            });
        });
    }

    #region Id

    public static readonly StyledProperty<long> IdProperty = AvaloniaProperty.Register<TagItem, long>(
        nameof(Id));

    public long Id
    {
        get => GetValue(IdProperty);
        set => SetValue(IdProperty, value);
    }

    #endregion

    #region 名称

    public static readonly StyledProperty<string?> TagNameProperty = AvaloniaProperty.Register<TagItem, string?>(
        nameof(TagName));

    public string? TagName
    {
        get => GetValue(TagNameProperty);
        set => SetValue(TagNameProperty, value);
    }

    #endregion

    #region 颜色

    public static readonly StyledProperty<string?> TagColorProperty = AvaloniaProperty.Register<TagItem, string?>(
        nameof(TagColor));

    public string? TagColor
    {
        get => GetValue(TagColorProperty);
        set => SetValue(TagColorProperty, value);
    }

    #endregion

    // public ICommand? Command { get; set; }

    #region 点击事件

    public static readonly StyledProperty<ICommand?> CommandProperty = AvaloniaProperty.Register<TagItem, ICommand?>(
        nameof(Command));

    public ICommand? Command
    {
        get => GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    #endregion
}
