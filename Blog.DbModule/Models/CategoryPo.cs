using System.ComponentModel.DataAnnotations;
using Common.Lib.Attributes;
using FreeSql.DataAnnotations;

namespace Blog.DbModule.Models;

[Table(Name = "category")]
public class CategoryPo
{
    /// <summary>
    /// 主键
    /// </summary>
    [Column(IsPrimary = true)]
    [Snowflake]
    public long Id { get; set; }

    #region Property

    /// <summary>
    /// 分类名称
    /// </summary>
    public string CategoryName { get; set; } = null!;

    public string? CategoryColor { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }

    #endregion

    #region Cretor

    /// <summary>
    /// 创建人
    /// </summary>
    public string? CreateBy { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime? CreateTime { get; set; }

    #endregion

    #region Editor

    /// <summary>
    /// 更新人
    /// </summary>
    public string? UpdateBy { get; set; }

    /// <summary>
    /// 更新时间
    /// </summary>
    public DateTime? UpdateTime { get; set; }

    #endregion
    
}
