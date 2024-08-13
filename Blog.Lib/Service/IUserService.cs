using Blog.Lib.Entity;
using Blog.Lib.Models;
using LanguageExt.Common;

namespace Blog.Lib.Service;

public interface IUserService
{
    Task<Result<Jwt>> Login(UserLoginDto user);
}