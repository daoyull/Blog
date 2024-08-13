using Common.Lib.Attributes;
using FreeSql.DataAnnotations;

namespace Blog.DbModule.Models;

[Table(Name = "blog")]
public class BlogPo
{
    public BlogPo()
    {
    }

    /// <summary>
    /// 主键
    /// </summary>
    [Snowflake]
    [Column(IsPrimary = true)]
    public long Id { get; set; }


    #region Property

    /// <summary>
    /// 标题
    /// </summary>
    public string Title { get; set; } = null!;

    /// <summary>
    /// 1 博客 2 友人帐 3 关于我
    /// </summary>
    public int Type { get; set; } = 1;

    /// <summary>
    /// 预览图
    /// </summary>
    public string? PreviewImg { get; set; }

    /// <summary>
    /// 分类
    /// </summary>
    public long CategoryId { get; set; }


    /// <summary>
    /// 描述
    /// </summary>
    public string? Description { get; set; }

    public bool IsPublished { get; set; }
    public bool IsRecommend { get; set; }
    public bool IsAppreciation { get; set; }

    /// <summary>
    /// 是否置顶
    /// </summary>
    public bool IsTop { get; set; }

    public bool IsComment { get; set; }
    
    public string? Password { get; set; }


    /// <summary>
    /// 查看数
    /// </summary>
    public int? Views { get; set; }

    /// <summary>
    /// 字数
    /// </summary>
    public int? Words { get; set; }

    /// <summary>
    /// 阅读时间(分钟)
    /// </summary>
    public int? ReadTime { get; set; }

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

    #region 导航查询

    public CategoryPo? Category { get; set; }


    [Navigate(ManyToMany = typeof(RelBlogTagPo))]
    public List<TagPo>? Tags { get; set; }

    #endregion
}