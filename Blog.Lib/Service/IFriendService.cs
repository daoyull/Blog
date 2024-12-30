using Blog.Lib.Models;
using Common.Lib.Models;


namespace Blog.Lib.Service;

public interface IFriendService
{
    Task<List<FriendVo>> GetFriendVoList(int count);
    Task<FriendContentVo> GetDesc();
    Task<PageResult<FriendVo>> GetFriendPage(FriendQueryDto friend);
    Task<bool> Published(long id, bool published);
    Task<int> AddAsync(FriendAddDto friend);
    Task<int> EditAsync(FriendEditDto friend);
    Task<int> EditFriendInfoContent(string content);
    Task<bool> EditCommentEnabled(bool commentEnabled);
}