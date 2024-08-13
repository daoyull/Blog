using Newtonsoft.Json;

namespace Blog.Lib.Entity;

public class Jwt
{
    [JsonIgnore] public string? Guid { get; set; }
    public string? Token { get; set; }

    public UserTokenVo? User { get; set; }

    [JsonIgnore] public long ExpireTime { get; set; }
}