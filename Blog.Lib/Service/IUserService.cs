using Blog.Lib.Entity;
using Blog.Lib.Models;


namespace Blog.Lib.Service;

public interface IUserService
{
    Task<Jwt> Login(UserLoginDto user);
}