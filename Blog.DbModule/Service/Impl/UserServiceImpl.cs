using Blog.DbModule.Constants;
using Blog.DbModule.Models;
using Blog.Lib.Entity;
using Blog.Lib.Models;
using Blog.Lib.Service;
using Common.FreeSql;
using Common.Lib.Exceptions;
using Common.Lib.Helpers;
using Common.Redis;
using LanguageExt.Common;
using Newtonsoft.Json;
using StackExchange.Redis;
using Blog.DbModule.Helper;

namespace Blog.DbModule.Service.Impl;

public class UserServiceImpl : IUserService
{
    private readonly IJwtService _jwtService;
    private readonly IFreeSql _db;
    private IDatabase? _redis;

    public UserServiceImpl(FreeSqlResolver resolver, IJwtService jwtService, RedisResolver redisResolver)
    {
        _jwtService = jwtService;
        _db = resolver.GetDatabase();
        redisResolver(BlogDbModule.BlogDatabaseName).IfSucc(db => _redis = db);
    }

    public async Task<Result<Jwt>> Login(UserLoginDto user)
    {
        var pass = Md5Helper.Md5(user.Password);
        var userPo = await _db.Select<UserPo>()
            .Where(it => it.Account == user.Account && it.Password == pass)
            .ToOneAsync();
        if (userPo == null)
        {
            throw new BusinessException("账号密码不匹配");
        }

        var jwtResult = await _jwtService.GenToken();

        async void SetRedis(Jwt jwt)
        {
            // 设置用户信息到缓存
            var userVo = new UserTokenVo
            {
                Account = user.Account,
                LoginTime = TimeHelper.NowCst,
                LoginExpireTime = TimeHelper.NowCst.AddHours(6),
                Email = userPo.Email,
                Phone = userPo.Phone,
                Sex = userPo.Sex ?? 2,
                Status = userPo.Status ?? 0
            };
            jwt.User = userVo;

            await _redis.StringSetAsync(BlogConstant.Token + jwt.Guid, JsonConvert.SerializeObject(userVo),
                TimeSpan.FromHours(6));
        }

        jwtResult.IfSucc(SetRedis);

        return jwtResult;
    }
}