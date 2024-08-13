using Blog.Lib.Entity;
using LanguageExt.Common;

namespace Blog.DbModule.Service;

public interface IJwtService
{
    Task<Result<Jwt>> GenToken();
}