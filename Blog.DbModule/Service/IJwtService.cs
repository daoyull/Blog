using Blog.Lib.Entity;


namespace Blog.DbModule.Service;

public interface IJwtService
{
    Task<Jwt> GenToken();
}