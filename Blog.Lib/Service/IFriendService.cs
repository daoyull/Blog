using Blog.Lib.Models;
using Common.Lib.Models;
using LanguageExt.Common;

namespace Blog.Lib.Service;

public interface IFriendService
{
    Task<Result<List<FriendVo>>> GetFriendVoList(int count);
    Task<Result<FriendContentVo>> GetDesc();
    Task<Result<PageResult<FriendVo>>> GetFriendPage(FriendQueryDto friend);
    Task<Result<bool>> Published(long id, bool published);
    Task<Result<int>> AddAsync(FriendAddDto friend);
    Task<Result<int>> EditAsync(FriendEditDto friend);
    Task<Result<int>> EditFriendInfoContent(string content);
    Task<Result<bool>> EditCommentEnabled(bool commentEnabled);
}