using Blog.Lib.Models;
using Newtonsoft.Json;

namespace Blog.Lib.Entity;

public class SiteInfo
{
    public List<Badge>? Badges { get; set; }
    [JsonProperty("siteInfo")] public FotterSiteInfo? FotterSiteInfo { get; set; }
    public LeftInformation? Introduction { get; set; }
    public List<NewBlog>? NewBlogList { get; set; }
    public List<RandomBlog>? RandomBlogList { get; set; }
    public List<TagCacheVo>? TagList { get; set; }
    public List<CategoryCacheVo>? CategoryList { get; set; }
}