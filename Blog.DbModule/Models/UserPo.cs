using FreeSql.DataAnnotations;

namespace Blog.DbModule.Models;

[Table(Name = "user")]
public class UserPo
{
    [Column(IsPrimary = true)] public string Id { get; set; } = null!;


    /// <summary>
    /// 昵称
    /// </summary>
    public string Account { get; set; } = null!;

    /// <summary>
    /// 邮箱
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// 手机号
    /// </summary>
    public string? Phone { get; set; }

    /// <summary>
    /// 性别
    /// 0 女 1 男 2 未知
    /// </summary>
    public int? Sex { get; set; }

    /// <summary>
    /// 登录密码
    /// </summary>
    public string? Password { get; set; }

    /// <summary>
    /// 账号状态
    /// </summary>
    public int? Status { get; set; }

    /// <summary>
    /// 创建人
    /// </summary>
    public string? CreateBy { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime? CreateTime { get; set; }

    /// <summary>
    /// 修改人
    /// </summary>
    public string? UpdateBy { get; set; }

    /// <summary>
    /// 修改时间
    /// </summary>
    public DateTime? UpdateTime { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

