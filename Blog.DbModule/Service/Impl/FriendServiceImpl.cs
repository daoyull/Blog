using Blog.DbModule.Models;
using Blog.Lib.Models;
using Blog.Lib.Service;
using Common.FreeSql;
using Common.Lib.Exceptions;
using Common.Lib.Models;
using FreeSql.Internal.Model;
using LanguageExt.Common;
using Mapster;
using Blog.DbModule.Helper;
namespace Blog.DbModule.Service.Impl;

public class FriendServiceImpl : IFriendService
{
    private readonly IFreeSql  _db;

    public FriendServiceImpl(FreeSqlResolver resolver)
    {
        _db = resolver.GetDatabase();
    }


    public async Task<Result<List<FriendVo>>> GetFriendVoList(int count)
    {
        return await _db.Select<FriendPo>()
            .OrderByRandom()
            .Take(count)
            .ToListAsync()
            .MapperTo<List<FriendPo>, List<FriendVo>>();
    }

    public async Task<Result<FriendContentVo>> GetDesc()
    {
        var blog = await _db.Select<BlogContentPo>()
            .Where(it => it.Type == 2)
            .OrderByDescending(it => it.CreateTime)
            .FirstAsync();
        return new FriendContentVo()
        {
            Content = blog.Content ?? "",
            CommentEnabled = blog.IsPublished
        };
    }

    public async Task<Result<PageResult<FriendVo>>> GetFriendPage(FriendQueryDto friend)
    {
        var pageInfo = friend.Adapt<BasePagingInfo>();
        var pages = await _db.Select<FriendPo>()
            .WhereIf(!string.IsNullOrEmpty(friend.Nickname), it => it.Nickname.Contains(friend.Nickname!))
            .Page(pageInfo)
            .ToListAsync()
            .MapperTo<List<FriendPo>, List<FriendVo>>();
        return new PageResult<FriendVo>(pageInfo.Count, pages);
    }

    public async Task<Result<bool>> Published(long id, bool published)
    {
        var rows = await _db.Update<FriendPo>()
            .Set(it => it.IsPublished, published)
            .Where(it => it.Id == id)
            .ExecuteAffrowsAsync();
        return rows == 1;
    }

    public async Task<Result<int>> AddAsync(FriendAddDto friend)
    {
        var friendPo = friend.Adapt<FriendPo>();
        var rows = await _db.Insert(friendPo).ExecuteAffrowsAsync();
        return rows;
    }

    public async Task<Result<int>> EditAsync(FriendEditDto friend)
    {
        var friendPo = friend.Adapt<FriendPo>();
        var rows = await _db.Update<FriendPo>()
            .SetSource(friendPo)
            .ExecuteAffrowsAsync();
        return rows;
    }

    public async Task<Result<int>> EditFriendInfoContent(string content)
    {
        var blogContentPo = new BlogContentPo()
        {
            Title = "FriendContent",
            Content = content,
            Type = 2,
            IsComment = false
        };
        using var uow = _db.CreateUnitOfWork();
        await uow.Orm.Delete<BlogPo>()
            .Where(it => it.Type == 2)
            .ExecuteAffrowsAsync();
        var rows = await uow.Orm.Insert(blogContentPo).ExecuteAffrowsAsync();
        uow.Commit();
        return rows;
    }

    public async Task<Result<bool>> EditCommentEnabled(bool commentEnabled)
    {
        var id = await _db.Select<BlogPo>().OrderByDescending(it => it.CreateTime)
            .Page(new BasePagingInfo() { PageNumber = 1, PageSize = 1 })
            .ToOneAsync(it => it.Id);
        if (id == 0)
        {
            return new Result<bool>(new BusinessException("未找到友人帐内容"));
        }

        var rows = await _db.Update<BlogPo>()
            .Set(it => it.IsPublished, commentEnabled)
            .Where(it => it.Id == id)
            .ExecuteAffrowsAsync();
        return rows == 1;
    }
}