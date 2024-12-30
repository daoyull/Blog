using Blog.DbModule.Models;
using Blog.Lib.Models;
using Blog.Lib.Service;
using Common.FreeSql;
using Common.Lib.Models;
using FreeSql.Internal.Model;

using Mapster;
using Blog.DbModule.Helper;
namespace Blog.DbModule.Service.Impl;

public class MomentServiceImpl : IMomentService
{
    private readonly IFreeSql  _db;

    public MomentServiceImpl(FreeSqlResolver resolver)
    {
        _db = resolver.GetDatabase();
    }

    public async Task<PageResult<MomentVo>> GetMomentPageList(MomentPageQueryDto query)
    {
        var pagingInfo = query.Adapt<BasePagingInfo>();
        var list = await _db.Select<MomentPo>()
            .Page(pagingInfo)
            .OrderByDescending(it=>it.CreateTime)
            .ToListAsync()
            .MapperTo<List<MomentPo>, List<MomentVo>>();
        return new PageResult<MomentVo>(pagingInfo.Count, list);
    }

    public async Task<bool> LikeMoment(long id)
    {
        var rows = await _db.Update<MomentPo>()
            .Set(it => new MomentPo()
            {
                Likes = it.Likes + 1
            })
            .Where(it => it.Id == id)
            .ExecuteAffrowsAsync();
        return rows == 1;
    }

    public async Task<bool> Published(long id, bool published)
    {
        var rows = await _db.Update<MomentPo>()
            .Set(it => it.IsPublished, published)
            .Where(it => it.Id == id)
            .ExecuteAffrowsAsync();
        return rows == 1;
    }

    public async Task<MomentVo> GetAsync(long id)
    {
        return await _db.Select<MomentPo>()
            .Where(it => it.Id == id)
            .ToOneAsync()
            .MapperTo<MomentPo, MomentVo>();
    }

    public async Task<int> EditAsync(MomentEditDto moment)
    {
        var po = moment.Adapt<MomentPo>();
        var rows = await _db.Update<MomentPo>()
            .SetSource(po)
            .ExecuteAffrowsAsync();
        return rows;
    }

    public async Task<int> AddAsync(MomentAddDto moment)
    {
        var po = moment.Adapt<MomentPo>();
        return await _db.Insert(po)
            .ExecuteAffrowsAsync();
    }

    public async Task<int> DeleteAsync(long id)
    {
        return await _db.Delete<MomentPo>()
            .Where(it => it.Id == id)
            .ExecuteAffrowsAsync();
    }
}