using Common.Lib.Attributes;
using Common.Lib.Helpers;
using FreeSql.DataAnnotations;

namespace Blog.DbModule.Models;

[Table(Name = "moment")]
public class MomentPo
{
    [Snowflake] [Column(IsPrimary = true)] public long Id { get; set; }


    /// <summary>
    /// 动态内容
    /// </summary>
    public string Content { get; set; } = null!;

    /// <summary>
    /// 点赞数量
    /// </summary>
    public int Likes { get; set; }

    public bool IsPublished { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    [Column(CanUpdate = false)]
    public DateTime? CreateTime { get; set; }

    /// <summary>
    /// 创建人
    /// </summary>
    public string? CreateBy { get; set; }
}
