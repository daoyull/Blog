using Newtonsoft.Json;

namespace Blog.Lib.Entity;

public  class FotterSiteInfo
{
    [JsonProperty("reward")]
    public string? Reward { get; set; }

    [JsonProperty("copyright")]
    public Copyright? Copyright { get; set; }

    [JsonProperty("blogName")]
    public string? BlogName { get; set; }

    [JsonProperty("beian")]
    public string? Beian { get; set; }

    [JsonProperty("webTitleSuffix")]
    public string? WebTitleSuffix { get; set; }

    [JsonProperty("footerImgTitle")]
    public string? FooterImgTitle { get; set; }

    [JsonProperty("commentAdminFlag")]
    public string? CommentAdminFlag { get; set; }

    [JsonProperty("footerImgUrl")]
    public string? FooterImgUrl { get; set; }
}

public partial class Copyright
{
    [JsonProperty("title")]
    public string? Title { get; set; }

    [JsonProperty("siteName")]
    public string? SiteName { get; set; }
}