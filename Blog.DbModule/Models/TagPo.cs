using Common.Lib.Attributes;
using FreeSql.DataAnnotations;

namespace Blog.DbModule.Models;

[Table(Name = "tag")]
public class TagPo
{
    [Snowflake] [Column(IsPrimary = true)] public long Id { get; set; }

    /// <summary>
    /// 标签名称
    /// </summary>
    public string TagName { get; set; } = null!;

    /// <summary>
    /// 标签颜色
    /// </summary>
    public string? TagColor { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }

    /// <summary>
    /// 创建人
    /// </summary>
    [Column(CanUpdate = false)]
    public string? CreateBy { get; set; }

    [Column(CanUpdate = false)] public DateTime CreateTime { get; set; }

    /// <summary>
    /// 更新人
    /// </summary>
    public string? UpdateBy { get; set; }

    public DateTime UpdateTime { get; set; }

    [Navigate(ManyToMany = typeof(RelBlogTagPo))]
    public List<BlogPo> Blogs { get; set; }

    [Navigate(ManyToMany = typeof(RelBlogTagPo))]
    public List<BlogContentPo> BlogContens { get; set; }
}