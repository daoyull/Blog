using Newtonsoft.Json;

namespace Blog.Lib.Entity;

public class Badge
{
    [JsonProperty("title")] public string? Title { get; set; }

    [JsonProperty("url")] public string? Url { get; set; }

    [JsonProperty("subject")] public string? Subject { get; set; }

    [JsonProperty("value")] public string? Value { get; set; }

    [JsonProperty("color")] public string? Color { get; set; }
}