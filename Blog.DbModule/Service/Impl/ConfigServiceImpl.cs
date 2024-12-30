using Blog.DbModule.Helper;
using Blog.DbModule.Models;
using Blog.Lib.Service;
using Common.FreeSql;
using Common.Lib.Exceptions;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Blog.DbModule.Service.Impl;

public class ConfigServiceImpl : IConfigService
{
    private readonly IFreeSql  _db;

    public ConfigServiceImpl(FreeSqlResolver resolver)
    {
        _db = resolver.GetDatabase();
    }

    public async Task<T> GetJsonConfig<T>(string key)
    {
        var t = await _db.Select<ConfigPo>()
            .Where(it => it.ConfigName == key)
            .ToOneAsync();

        if (t == null)
        {
            throw new BusinessException("未定义的配置");
        }

        return JsonConvert.DeserializeObject<T>(t.ConfigValue!)!;
    }

    public async Task<JObject> GetJsonConfig(string key)
    {
        var t = await _db.Select<ConfigPo>()
            .Where(it => it.ConfigName == key)
            .ToOneAsync();
        if (t == null)
        {
            throw new BusinessException("未定义的配置");
        }

        return JsonConvert.DeserializeObject<JObject>(t.ConfigValue!)!;
    }
}