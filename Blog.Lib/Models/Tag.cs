using System.ComponentModel.DataAnnotations;
using Common.Lib.Service;
using Common.Mvvm.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Blog.Lib.Models;

#region DTO

public partial class TagQueryDto : ObservableObject
{
    /// <summary>
    /// 标签ID
    /// </summary>
    [ObservableProperty] private long? _id;

    /// <summary>
    /// 标签名称
    /// </summary>
    [ObservableProperty] private string? _tagName;
}

public partial class TagPageQueryDto : MvvmPageQuery
{
    /// <summary>
    /// 标签ID
    /// </summary>
    [ObservableProperty] private long? _id;

    /// <summary>
    /// 标签名称
    /// </summary>
    [ObservableProperty] private string? _tagName;
}

public partial class TagAddDto : ObservableObject, IDataVerify
{
    /// <summary>
    /// 主键
    /// </summary>
    [ObservableProperty] private long? _id;


    /// <summary>
    /// 标签名称
    /// </summary>
    [ObservableProperty] [property: Required(ErrorMessage = "标签名称不可为空")]
    private string _tagName = null!;

    /// <summary>
    /// 标签颜色
    /// </summary>
    [ObservableProperty] private string? _tagColor;

    /// <summary>
    /// 备注
    /// </summary>
    [ObservableProperty] private string? _remark;

    /// <summary>
    /// 创建人
    /// </summary>
    [ObservableProperty] private string? _createBy;

    /// <summary>
    /// 创建时间
    /// </summary>
    [ObservableProperty] private DateTime? _createTime;
}

public partial class TagEditDto : ObservableObject
{
    /// <summary>
    /// 主键
    /// </summary>
    [ObservableProperty] private long? _id;

    /// <summary>
    /// 标签名称
    /// </summary>
    [ObservableProperty] [property: Required(ErrorMessage = "标签名称不可为空")]
    private string _tagName = null!;

    /// <summary>
    /// 标签颜色
    /// </summary>
    [ObservableProperty] private string? _tagColor;

    /// <summary>
    /// 备注
    /// </summary>
    [ObservableProperty] private string? _remark;

    /// <summary>
    /// 更新人
    /// </summary>
    [ObservableProperty] private string? _updateBy;

    /// <summary>
    /// 更新时间
    /// </summary>
    [ObservableProperty] private DateTime? _updateTime;
}

#endregion

#region Vo

public partial class TagVo : ObservableObject
{
    /// <summary>
    /// 标签ID
    /// </summary>
    [ObservableProperty] private long? _id;

    /// <summary>
    /// 标签名称
    /// </summary>
    [ObservableProperty] private string? _tagName;

    /// <summary>
    /// 标签颜色
    /// </summary>
    [ObservableProperty] private string? _tagColor;

    /// <summary>
    /// 备注
    /// </summary>
    [ObservableProperty] private string? _remark;


    /// <summary>
    /// 创建人
    /// </summary>
    [ObservableProperty] private string? _createBy;

    /// <summary>
    /// 创建时间
    /// </summary>
    [ObservableProperty] private DateTime? _createTime;


    /// <summary>
    /// 更新人
    /// </summary>
    [ObservableProperty] private string? _updateBy;

    /// <summary>
    /// 更新时间
    /// </summary>
    [ObservableProperty] private DateTime? _updateTime;
}

public class TagCacheVo
{
    public long Id { get; set; }

    public string? TagName { get; set; }

    public string? TagColor { get; set; }
}

#endregion