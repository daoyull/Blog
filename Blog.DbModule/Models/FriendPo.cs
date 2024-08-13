using Common.Lib.Attributes;
using Common.Lib.Helpers;
using FreeSql.DataAnnotations;

namespace Blog.DbModule.Models;

[Table(Name = "friend")]
public class FriendPo
{
    [Column(IsPrimary = true)] [Snowflake] public long Id { get; set; }

    /// <summary>
    /// 昵称
    /// </summary>
    public string Nickname { get; set; } = null!;

    /// <summary>
    /// 描述
    /// </summary>
    public string Description { get; set; } = null!;

    /// <summary>
    /// 站点
    /// </summary>
    public string Website { get; set; } = null!;

    /// <summary>
    /// 头像
    /// </summary>
    public string Avatar { get; set; } = null!;

    /// <summary>
    /// 公开或隐藏
    /// </summary>
    public bool IsPublished { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime? CreateTime { get; set; }

    /// <summary>
    /// 创建人
    /// </summary>
    public string? CreateBy { get; set; }
}
