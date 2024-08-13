using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media;
using Blog.Lib.Models;
using BlogView.Service;
using Common.Lib.Ioc;
using CommunityToolkit.Mvvm.Input;

namespace BlogView.Controls;

[TemplatePart(PART_CategoryColor, typeof(Panel))]
public class BlogCardItem : TemplatedControl
{
    #region 字段

    private Panel? _catrgotyColorPath;

    #endregion

    #region 命名

    public const string PART_CategoryColor = "BlogCardItem_CategoryColor";

    #endregion

    static BlogCardItem()
    {
        CategoryProperty.Changed.AddClassHandler<BlogCardItem>((item, _) => item.RefreshUi(item));
        TagsProperty.Changed.AddClassHandler<BlogCardItem>((item, _) => item.RefreshUi(item));
    }

    public BlogCardItem()
    {
        ReadCommand = new RelayCommand(RouterBlogDetail);
        Category = new CategoryCacheVo
        {
            Id = 0,
            CategoryName = "test",
            CategoryColor = "#FF5E55"
        };
        Description = "12312312";
        Tags = new List<TagCacheVo>()
        {
            new TagCacheVo
            {
                Id = 0,
                TagName = "test",
                TagColor = "#FF5E55"
            }
        };
    }

    public void CategoryClick(object? sender, PointerPressedEventArgs e)
    {
        if (Category != null)
        {
            Ioc.Resolve<PageService>().RouterCategoryBlogList(Category);
        }
    }

    private async void RouterBlogDetail()
    {
        Ioc.Resolve<PageService>().RouterBlogDetail(Id);
    }

    #region Id

    public static readonly StyledProperty<long> IdProperty = AvaloniaProperty.Register<BlogCardItem, long>(
        nameof(Id));

    public long Id
    {
        get => GetValue(IdProperty);
        set => SetValue(IdProperty, value);
    }

    #endregion

    #region 标题

    public static readonly StyledProperty<string?> TitleProperty = AvaloniaProperty.Register<BlogCardItem, string?>(
        nameof(Title));

    public string? Title
    {
        get => GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    #endregion

    #region 标签

    public static readonly StyledProperty<ICollection<TagCacheVo>?> TagsProperty =
        AvaloniaProperty.Register<BlogCardItem, ICollection<TagCacheVo>?>(
            nameof(Tags));

    public ICollection<TagCacheVo>? Tags
    {
        get => GetValue(TagsProperty);
        set => SetValue(TagsProperty, value);
    }

    #endregion

    #region 分类

    public static readonly StyledProperty<CategoryCacheVo?> CategoryProperty =
        AvaloniaProperty.Register<BlogCardItem, CategoryCacheVo?>(
            nameof(Category));

    public CategoryCacheVo? Category
    {
        get => GetValue(CategoryProperty);
        set => SetValue(CategoryProperty, value);
    }

    #endregion

    #region 创建时间

    public static readonly StyledProperty<DateTime> CreateTimeProperty =
        AvaloniaProperty.Register<BlogCardItem, DateTime>(
            nameof(CreateTime));

    public DateTime CreateTime
    {
        get => GetValue(CreateTimeProperty);
        set => SetValue(CreateTimeProperty, value);
    }

    #endregion

    #region 热度

    public static readonly StyledProperty<int?> ViewsProperty = AvaloniaProperty.Register<BlogCardItem, int?>(
        nameof(Views));

    public int? Views
    {
        get => GetValue(ViewsProperty);
        set => SetValue(ViewsProperty, value);
    }

    #endregion

    #region 说明

    public static readonly StyledProperty<string?> DescriptionProperty =
        AvaloniaProperty.Register<BlogCardItem, string?>(
            nameof(Description));

    public string? Description
    {
        get => GetValue(DescriptionProperty);
        set => SetValue(DescriptionProperty, value);
    }

    #endregion

    #region 是否置顶

    public static readonly StyledProperty<bool> IsTopProperty = AvaloniaProperty.Register<BlogCardItem, bool>(
        nameof(IsTop));

    public bool IsTop
    {
        get => GetValue(IsTopProperty);
        set => SetValue(IsTopProperty, value);
    }

    #endregion

    #region 字数

    public static readonly StyledProperty<int> WordsProperty = AvaloniaProperty.Register<BlogCardItem, int>(
        nameof(Words));

    public int Words
    {
        get => GetValue(WordsProperty);
        set => SetValue(WordsProperty, value);
    }

    #endregion

    #region 阅读时间

    public static readonly StyledProperty<int> ReadTimeProperty = AvaloniaProperty.Register<BlogCardItem, int>(
        nameof(ReadTime));

    public int ReadTime
    {
        get => GetValue(ReadTimeProperty);
        set => SetValue(ReadTimeProperty, value);
    }

    #endregion

    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);
        _catrgotyColorPath = e.NameScope.Find<Panel>(PART_CategoryColor);
        if (_catrgotyColorPath != null)
        {
            _catrgotyColorPath.PointerPressed += CategoryClick;
        }

        RefreshUi(this);
    }

    private void RefreshUi(BlogCardItem item)
    {
        if (item._catrgotyColorPath != null && item.Category != null)
        {
            if (!string.IsNullOrEmpty(item.Category.CategoryColor))
            {
                item._catrgotyColorPath.Background = SolidColorBrush.Parse(item.Category.CategoryColor);
                item._catrgotyColorPath.Tag = item.Category.CategoryName;
            }
        }
    }


    public static readonly StyledProperty<ICommand?> ReadCommandProperty =
        AvaloniaProperty.Register<BlogCardItem, ICommand?>(
            nameof(ReadCommand));

    public ICommand? ReadCommand
    {
        get => GetValue(ReadCommandProperty);
        set => SetValue(ReadCommandProperty, value);
    }

    protected override void OnUnloaded(RoutedEventArgs e)
    {
        if (_catrgotyColorPath != null)
        {
            _catrgotyColorPath.PointerPressed -= CategoryClick;
        }

        base.OnUnloaded(e);
    }
}
