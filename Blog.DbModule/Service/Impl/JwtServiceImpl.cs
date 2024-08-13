﻿using Blog.DbModule.Helper;
using Blog.Lib.Entity;
using Blog.Lib.Service;
using LanguageExt.Common;

namespace Blog.DbModule.Service.Impl;

public class JwtServiceImpl : IJwtService
{
    public async Task<Result<Jwt>> GenToken()
    {
        var guid = Guid.NewGuid().ToString();
        var token = JwtHelper.GetJwtToken(guid);
        return new Jwt
        {
            Guid = guid,
            Token = token
        };
    }
}