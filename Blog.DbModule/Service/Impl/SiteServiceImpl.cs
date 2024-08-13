using Blog.Lib.Entity;
using Blog.Lib.Service;
using Common.FreeSql;
using Common.Redis;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace Blog.DbModule.Service.Impl;

public class SiteInfoServiceImpl : ISiteService
{
    private readonly IInformationService _informationService;
    private readonly IBlogService _blogService;
    private readonly ITagService _tagService;
    private readonly ICategoryService _categoryService;
    private readonly IDatabase _db;

    public SiteInfoServiceImpl(RedisResolver resolver, IInformationService informationService,
        IBlogService blogService, ITagService tagService, ICategoryService categoryService)
    {
        _informationService = informationService;
        _blogService = blogService;
        _tagService = tagService;
        _categoryService = categoryService;
        _db = resolver(BlogDbModule.BlogDatabaseName);
    }

    public async Task<SiteInfo> GetSiteInfoAsync()
    {
        var stringGetAsync = await _db.StringGetAsync("blog:site");
        if (!stringGetAsync.HasValue)
        {
            return await RefreshSiteInfoAsync();
        }

        return JsonConvert.DeserializeObject<SiteInfo>(stringGetAsync.ToString())!;
    }

    public async Task<SiteInfo> RefreshSiteInfoAsync()
    {
        var siteInfo = new SiteInfo();
        (await _informationService.GetBadges()).IfSucc(re => siteInfo.Badges = re);
        (await _informationService.GetSiteInfo()).IfSucc(re => siteInfo.FotterSiteInfo = re);
        (await _informationService.GetLeftInformation()).IfSucc(re => siteInfo.Introduction = re);
        (await _blogService.QueryNewBlog(5)).IfSucc(re => siteInfo.NewBlogList = re);
        (await _blogService.QueryRandomBlogs(3)).IfSucc(re => siteInfo.RandomBlogList = re);
        (await _tagService.GetCacheListAsync()).IfSucc(re => siteInfo.TagList = re);
        (await _categoryService.GetCacheListAsync()).IfSucc(re => siteInfo.CategoryList = re);
        await _db.StringSetAsync("blog:site", JsonConvert.SerializeObject(siteInfo));
        return siteInfo;
    }
}