using System.ComponentModel.DataAnnotations;
using Common.Lib.Service;
using Common.Mvvm.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Blog.Lib.Models;

public partial class MomentVo : ObservableObject
{
    /// <summary>
    /// Id
    /// </summary>
    [ObservableProperty] private long? _id;

    /// <summary>
    /// 动态内容
    /// </summary>
    [ObservableProperty] private string? _content;

    /// <summary>
    /// 点赞数量
    /// </summary>
    [ObservableProperty] private int? _likes;

    /// <summary>
    /// 创建时间
    /// </summary>
    [ObservableProperty] private DateTime? _createTime;
    
    [ObservableProperty] private bool _isPublished;

    /// <summary>
    /// 创建人
    /// </summary>
    [ObservableProperty] private string? _createBy;
}

public partial class MomentPageQueryDto : MvvmPageQuery
{
    [ObservableProperty] private long? _id;
    [ObservableProperty] private string? _createBy;
}

public partial class MomentQueryDto : ObservableObject
{
    [ObservableProperty] private long? _id;
    [ObservableProperty] private string? _createBy;
}

public partial class MomentAddDto : ObservableObject, IDataVerify
{
    /// <summary>
    /// 主键
    /// </summary>
    [ObservableProperty] private long? _id;


    /// <summary>
    /// 动态内容
    /// </summary>
    [property: Required(ErrorMessage = "内容不可为空")] [ObservableProperty]
    private string? _content;

    [ObservableProperty] private bool _isPublished;

    /// <summary>
    /// 点赞数量
    /// </summary>
    [ObservableProperty] private int? _likes;
}

public partial class MomentEditDto : ObservableObject, IDataVerify
{
    /// <summary>
    /// 主键
    /// </summary>
    [ObservableProperty] private long _id;


    /// <summary>
    /// 动态内容
    /// </summary>
    [property: Required(ErrorMessage = "内容不可为空")] [ObservableProperty]
    private string? _content;
    
    [ObservableProperty] private bool _isPublished;

    /// <summary>
    /// 点赞数量
    /// </summary>
    [ObservableProperty] private int? _likes;
}