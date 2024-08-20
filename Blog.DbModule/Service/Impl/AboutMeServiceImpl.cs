using Blog.DbModule.Helper;
using Blog.DbModule.Models;
using Blog.Lib.Service;
using Common.FreeSql;
using LanguageExt.Common;

namespace Blog.DbModule.Service.Impl;

public class AboutMeServiceImpl : IAboutMeService
{
    private readonly IFreeSql _db;

    public AboutMeServiceImpl(FreeSqlResolver resolver)
    {
        _db = resolver.GetDatabase();
    }

    public async Task<Result<string>> Desc()
    {
        var blog = await _db.Select<BlogContentPo>()
            .Where(it => it.Type == 3)
            .OrderByDescending(it => it.CreateTime)
            .FirstAsync();

        return blog?.Content ?? "";
    }
}