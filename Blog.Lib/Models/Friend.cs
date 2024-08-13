using Common.Lib.Service;
using Common.Mvvm.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Blog.Lib.Models
{
    public partial class FriendVo : ObservableObject
    {
        /// <summary>
        /// Id
        /// </summary>
        [ObservableProperty] private long? _id;

        /// <summary>
        /// 昵称
        /// </summary>
        [ObservableProperty] private string? _nickname;

        /// <summary>
        /// 描述
        /// </summary>
        [ObservableProperty] private string? _description;

        /// <summary>
        /// 站点
        /// </summary>
        [ObservableProperty] private string? _website;

        /// <summary>
        /// 头像
        /// </summary>
        [ObservableProperty] private string? _avatar;

        /// <summary>
        /// 公开或隐藏
        /// </summary>
        [ObservableProperty] private bool? _isPublished;

        /// <summary>
        /// 创建时间
        /// </summary>
        [ObservableProperty] private DateTime? _createTime;

        /// <summary>
        /// 创建人
        /// </summary>
        [ObservableProperty] private string? _createBy;

        public string? Color { get; set; }
    }

    public partial class FriendPageQueryDto : MvvmPageQuery
    {
        /// <summary>
        /// Id
        /// </summary>
        [ObservableProperty] private long? _id;

        /// <summary>
        /// 昵称
        /// </summary>
        [ObservableProperty] private string? _nickName;
    }

    public partial class FriendQueryDto : ObservableObject
    {
        /// <summary>
        /// Id
        /// </summary>
        [ObservableProperty] private long? _id;

        /// <summary>
        /// 昵称
        /// </summary>
        [ObservableProperty] private string? _nickname;
    }


    public partial class FriendAddDto : ObservableObject, IDataVerify
    {
        /// <summary>
        /// 主键
        /// </summary>
        [ObservableProperty] private long? _id;


        /// <summary>
        /// 昵称
        /// </summary>
        [ObservableProperty] private string? _nickname;

        /// <summary>
        /// 描述
        /// </summary>
        [ObservableProperty] private string? _description;

        /// <summary>
        /// 站点
        /// </summary>
        [ObservableProperty] private string? _website;

        /// <summary>
        /// 头像
        /// </summary>
        [ObservableProperty] private string? _avatar;

        /// <summary>
        /// 公开或隐藏
        /// </summary>
        [ObservableProperty] private bool? _isPublished;
    }


    public partial class FriendEditDto : ObservableObject, IDataVerify
    {
        /// <summary>
        /// 主键
        /// </summary>
        [ObservableProperty] private long _id;


        /// <summary>
        /// 昵称
        /// </summary>
        [ObservableProperty] private string? _nickname;

        /// <summary>
        /// 描述
        /// </summary>
        [ObservableProperty] private string? _description;

        /// <summary>
        /// 站点
        /// </summary>
        [ObservableProperty] private string? _website;

        /// <summary>
        /// 头像
        /// </summary>
        [ObservableProperty] private string? _avatar;

        /// <summary>
        /// 公开或隐藏
        /// </summary>
        [ObservableProperty] private bool? _isPublished;
    }

    public partial class FriendContentDto : ObservableObject
    {
        [ObservableProperty] private string? _content;
    }
    
    public partial class FriendContentVo : ObservableObject
    {
        [ObservableProperty] private string? _content;
        [ObservableProperty] private bool _commentEnabled;
        
    }
}