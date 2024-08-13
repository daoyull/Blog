using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using Common.Lib.Service;
using Common.Mvvm.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Blog.Lib.Models
{
    public partial class BlogVo : ObservableObject
    {
        #region Property

        /// <summary>
        /// 博客Id
        /// </summary>
        [ObservableProperty] private long? _id;

        /// <summary>
        /// 标题
        /// </summary>
        [ObservableProperty] private string? _title;

        /// <summary>
        /// 预览图
        /// </summary>
        [ObservableProperty] private string? _previewImg;

        /// <summary>
        /// 分类
        /// </summary>
        [ObservableProperty] private long _categoryId;

        /// <summary>
        /// 内容
        /// </summary>
        [ObservableProperty] private string? _content;

        /// <summary>
        /// 描述
        /// </summary>
        [ObservableProperty] private string? _description;

        /// <summary>
        /// 是否置顶
        /// </summary>
        [ObservableProperty] private bool? _isTop;

        [ObservableProperty] private bool? _isAppreciation;
        [ObservableProperty] private bool? _isComment;
        [ObservableProperty] private bool? _isPublished;
        [ObservableProperty] private bool? _isRecommend;

        /// <summary>
        /// 查看数
        /// </summary>
        [ObservableProperty] private int? _views;

        /// <summary>
        /// 字数
        /// </summary>
        [ObservableProperty] private int? _words;

        /// <summary>
        /// 阅读时间(分钟)
        /// </summary>
        [ObservableProperty] private int? _readTime;

        /// <summary>
        /// 备注
        /// </summary>
        [ObservableProperty] private string? _remark;

        [ObservableProperty] private string? _password;

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

        #region 扩展

        [ObservableProperty] private CategoryVo? _category;
        [ObservableProperty] private ObservableCollection<TagVo>? _tags;

        #endregion
    }

    public partial class BlogPageQueryDto : MvvmPageQuery
    {
        [ObservableProperty] private long? _id;
        [ObservableProperty] private string? _title;
        [ObservableProperty] private long? _categoryId;
        [ObservableProperty] private long? _tagId;
    }

    public partial class BlogQueryDto : ObservableObject
    {
        [ObservableProperty] private long? _id;
        [ObservableProperty] private string? _title;
        [ObservableProperty] private long? _categoryId;
        [ObservableProperty] private long? _tagId;
    }

    public partial class BlogEditVisibilityDto : ObservableObject
    {
        [ObservableProperty] private bool? _isTop;

        [ObservableProperty] private bool? _isAppreciation;
        [ObservableProperty] private bool? _isComment;
        [ObservableProperty] private bool _isPublished;
        [ObservableProperty] private bool _isRecommend;
        [ObservableProperty] private string? _password;
    }


    public partial class BlogAddDto : ObservableObject, IDataVerify
    {
        /// <summary>
        /// 主键
        /// </summary>
        [ObservableProperty] private long? _id;

        #region Property

        /// <summary>
        /// 标题
        /// </summary>
        [ObservableProperty] [property: Required(ErrorMessage = "标题不可为空")]
        private string? _title;

        [ObservableProperty] private string? _previewImg;

        /// <summary>
        /// 内容
        /// </summary>
        [ObservableProperty] private string? _content;

        /// <summary>
        /// 描述
        /// </summary>
        [ObservableProperty] private string? _description;

        /// <summary>
        /// 是否置顶
        /// </summary>
        [ObservableProperty] private bool _isTop;

        [ObservableProperty] private bool _isAppreciation;
        [ObservableProperty] private bool _isComment;
        [ObservableProperty] private bool _isPublished;
        [ObservableProperty] private bool _isRecommend;
        [ObservableProperty] private string? _password;

        /// <summary>
        /// 查看数
        /// </summary>
        [ObservableProperty] private int? _views;

        /// <summary>
        /// 字数
        /// </summary>
        [ObservableProperty] private int? _words;

        /// <summary>
        /// 阅读时间(分钟)
        /// </summary>
        [ObservableProperty] private int? _readTime;

        /// <summary>
        /// 备注
        /// </summary>
        [ObservableProperty] private string? _remark;

        #endregion


        #region 扩展

        [ObservableProperty] private long _categoryId;

        [ObservableProperty] private List<long> _tagIds = new();

        #endregion
    }


    public partial class BlogEditDto : ObservableObject, IDataVerify
    {
        /// <summary>
        /// 主键
        /// </summary>
        [ObservableProperty] private long? _id;

        #region Property

        /// <summary>
        /// 标题
        /// </summary>
        [property: Required(ErrorMessage = "标题不可为空")] [ObservableProperty]
        private string? _title;

        [ObservableProperty] private string? _previewImg;

        /// <summary>
        /// 内容
        /// </summary>
        [ObservableProperty] private string? _content;

        /// <summary>
        /// 描述
        /// </summary>
        [ObservableProperty] private string? _description;

        /// <summary>
        /// 是否置顶
        /// </summary>
        [ObservableProperty] private bool _isTop;

        [ObservableProperty] private bool _isAppreciation;
        [ObservableProperty] private bool _isComment;
        [ObservableProperty] private bool _isPublished;
        [ObservableProperty] private bool _isRecommend;
        [ObservableProperty] private string? _password;

        /// <summary>
        /// 查看数
        /// </summary>
        [ObservableProperty] private int? _views;

        /// <summary>
        /// 字数
        /// </summary>
        [ObservableProperty] private int? _words;

        /// <summary>
        /// 阅读时间(分钟)
        /// </summary>
        [ObservableProperty] private int? _readTime;

        /// <summary>
        /// 备注
        /// </summary>
        [ObservableProperty] private string? _remark;

        #endregion

        #region 扩展

        [ObservableProperty] private long? _categoryId;

        [ObservableProperty] private List<long> _tagIds = new();
        

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

    public partial class BlogCardVo : ObservableObject
    {
        #region Property

        /// <summary>
        /// 博客Id
        /// </summary>
        [ObservableProperty] private long? _id;

        /// <summary>
        /// 标题
        /// </summary>
        [ObservableProperty] private string? _title;

        /// <summary>
        /// 预览图
        /// </summary>
        [ObservableProperty] private string? _previewImg;

        /// <summary>
        /// 分类
        /// </summary>
        [ObservableProperty] private long _categoryId;

        /// <summary>
        /// 内容
        /// </summary>
        [ObservableProperty] private string? _content;

        /// <summary>
        /// 描述
        /// </summary>
        [ObservableProperty] private string? _description;

        /// <summary>
        /// 是否置顶
        /// </summary>
        [ObservableProperty] private bool? _isTop;

        [ObservableProperty] private bool? _isAppreciation;
        [ObservableProperty] private bool? _isComment;
        [ObservableProperty] private bool _isPublished;
        [ObservableProperty] private bool _isRecommend;

        /// <summary>
        /// 查看数
        /// </summary>
        [ObservableProperty] private int? _views;

        /// <summary>
        /// 字数
        /// </summary>
        [ObservableProperty] private int? _words;

        /// <summary>
        /// 阅读时间(分钟)
        /// </summary>
        [ObservableProperty] private int? _readTime;

        /// <summary>
        /// 备注
        /// </summary>
        [ObservableProperty] private string? _remark;

        [ObservableProperty] private string? _password;

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

        #region 扩展

        [ObservableProperty] private CategoryCacheVo? _category;
        [ObservableProperty] private ObservableCollection<TagCacheVo>? _tags;

        #endregion
    }
}