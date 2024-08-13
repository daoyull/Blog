using FreeSql.DataAnnotations;

namespace Blog.DbModule.Models;

[Table(Name = "rel_blog_tag")]
public class RelBlogTagPo
{
    #region Property

    /// <summary>
    /// TagID
    /// </summary>
    [Column(IsPrimary = true)]
    public long TagId { get; set; }

    /// <summary>
    /// 博客ID
    /// </summary>
    [Column(IsPrimary = true)]
    public long BlogId { get; set; }

    [Navigate(nameof(TagId))] public TagPo Tag { get; set; }

    [Navigate(nameof(BlogId))] public BlogPo Blog { get; set; }

    [Navigate(nameof(BlogId))] public BlogContentPo BlogConten { get; set; }

    #endregion
}
