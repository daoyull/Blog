using Newtonsoft.Json;

namespace Blog.Lib.Entity;

public class UserTokenVo
{
    [JsonProperty("nickName")] public string? Account { get; set; }

    [JsonProperty("loginType")] public int? LoginType { get; set; }

    [JsonProperty("loginTime")] public DateTime? LoginTime { get; set; }

    [JsonProperty("loginExpireTime")] public DateTime? LoginExpireTime { get; set; }

    [JsonProperty("email")] public string? Email { get; set; }

    [JsonProperty("phone")] public string? Phone { get; set; }

    [JsonProperty("sex")] public int Sex { get; set; }

    [JsonProperty("status")] public int Status { get; set; }
}