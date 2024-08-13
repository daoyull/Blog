using System.ComponentModel.DataAnnotations;
using Common.Lib.Service;
using Common.Mvvm.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Blog.Lib.Models;

public partial class CategoryVo : ObservableObject, IDataVerify
{
    #region Property

    /// <summary>
    /// 分类Id
    /// </summary>
    [ObservableProperty] private long? _id;

    /// <summary>
    /// 分类名称
    /// </summary>
    [ObservableProperty] private string? _categoryName;

    [ObservableProperty] private string? _categoryColor;

    /// <summary>
    /// 备注
    /// </summary>
    [ObservableProperty] private string? _remark;

    #endregion

    #region Cretor

    /// <summary>
    /// 创建人
    /// </summary>
    [ObservableProperty] private string? _createBy;

    /// <summary>
    /// 创建时间
    /// </summary>
    [ObservableProperty] private DateTime? _createTime;

    #endregion

    #region Editor

    /// <summary>
    /// 更新人
    /// </summary>
    [ObservableProperty] private string? _updateBy;

    /// <summary>
    /// 更新时间
    /// </summary>
    [ObservableProperty] private DateTime? _updateTime;

    #endregion
}

public partial class CategoryPageQueryDto : MvvmPageQuery
{
    /// <summary>
    /// 分类Id
    /// </summary>
    [ObservableProperty] private long? _id;

    /// <summary>
    /// 分类名称
    /// </summary>
    [ObservableProperty] private string? _categoryName;
}

public partial class CategoryQueryDto : ObservableObject
{
    /// <summary>
    /// 分类Id
    /// </summary>
    [ObservableProperty] private long? _id;

    /// <summary>
    /// 分类名称
    /// </summary>
    [ObservableProperty] private string? _categoryName;
}

public partial class CategoryAddDto : ObservableObject, IDataVerify
{
    /// <summary>
    /// 主键
    /// </summary>
    [ObservableProperty] private long? _id;

    #region Property

    /// <summary>
    /// 分类名称
    /// </summary>
    [ObservableProperty] [property: Required(ErrorMessage = "分类名称不可为空")]
    private string? _categoryName;

    [ObservableProperty] private string? _categoryColor;

    /// <summary>
    /// 备注
    /// </summary>
    [ObservableProperty] private string? _remark;

    #endregion

    #region Cretor

    /// <summary>
    /// 创建人
    /// </summary>
    [ObservableProperty] private string? _createBy;

    /// <summary>
    /// 创建时间
    /// </summary>
    [ObservableProperty] private DateTime? _createTime;

    #endregion
}

public partial class CategoryEditDto : ObservableObject, IDataVerify
{
    /// <summary>
    /// 主键
    /// </summary>
    [ObservableProperty] private long? _id;

    #region Property

    /// <summary>
    /// 分类名称
    /// </summary>
    [ObservableProperty]
    [property: MinLength(1, ErrorMessage = "分类名称不可为空")]
    [property: Required(ErrorMessage = "分类名称不可为空")]
    private string? _categoryName;

    [ObservableProperty] private string? _categoryColor;

    /// <summary>
    /// 备注
    /// </summary>
    [ObservableProperty] private string? _remark;

    #endregion

    #region Editor

    /// <summary>
    /// 更新人
    /// </summary>
    [ObservableProperty] private string? _updateBy;

    /// <summary>
    /// 更新时间
    /// </summary>
    [ObservableProperty] private DateTime? _updateTime;

    #endregion
}

public class CategoryCacheVo
{
    public long Id { get; set; }

    public string? CategoryName { get; set; }

    public string? CategoryColor { get; set; }
}