using FreeSql.DataAnnotations;

namespace Blog.DbModule.Models;

[Table(Name = "blog")]
public class BlogContentPo : BlogPo
{
    /// <summary>
    /// 内容
    /// </summary>
    public string? Content { get; set; }
}
