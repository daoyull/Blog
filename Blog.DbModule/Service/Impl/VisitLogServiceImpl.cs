using Blog.DbModule.Models;
using Blog.Lib.Models;
using Blog.Lib.Service;
using Common.FreeSql;

using Mapster;
using Blog.DbModule.Helper;
namespace Blog.DbModule.Service.Impl;

public class VisitLogServiceImpl : IVisitLogService
{
    private readonly IFreeSql  _db;

    public VisitLogServiceImpl(FreeSqlResolver resolver)
    {
        _db = resolver.GetDatabase();
    }

    public async Task<int> AddAsync(VisitLogModel visitLogModel)
    {
        var po = visitLogModel.Adapt<VisitLogPo>();
        var rows = await _db.Insert(po).ExecuteAffrowsAsync();
        return rows;
    }
}