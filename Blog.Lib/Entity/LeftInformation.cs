using Newtonsoft.Json;

namespace Blog.Lib.Entity;

public class LeftInformation
{
    [JsonProperty("title")] public string? Title { get; set; }

    [JsonProperty("rollText")] public string[]? RollText { get; set; }

    [JsonProperty("github")] public string? Github { get; set; }

    [JsonProperty("telegram")] public string? Telegram { get; set; }

    [JsonProperty("qq")] public string? Qq { get; set; }

    [JsonProperty("bilibili")] public string? Bilibili { get; set; }

    [JsonProperty("netease")] public string? Netease { get; set; }

    [JsonProperty("email")] public string? Email { get; set; }

    [JsonProperty("favorites")] public List<Favorite>? Favorites { get; set; }
}

public class Favorite
{
    [JsonProperty("title")] public string? Title { get; set; }

    [JsonProperty("content")] public string? Content { get; set; }
}
