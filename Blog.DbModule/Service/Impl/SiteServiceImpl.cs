using Blog.DbModule.Constants;
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
    private IDatabase? _redis;

    public SiteInfoServiceImpl(RedisResolver redisResolver, IInformationService informationService,
        IBlogService blogService, ITagService tagService, ICategoryService categoryService)
    {
        _informationService = informationService;
        _blogService = blogService;
        _tagService = tagService;
        _categoryService = categoryService;
        _redis = redisResolver(BlogDbModule.BlogDatabaseName);
    }

    public async Task<SiteInfo> GetSiteInfoAsync()
    {
        if (_redis != null)
        {
            var redisValue = await _redis.StringGetAsync(BlogConstant.Site);
            if (!redisValue.HasValue)
            {
                return await RefreshSiteInfoAsync();
            }

            return JsonConvert.DeserializeObject<SiteInfo>(redisValue.ToString())!;
        }

        return await GetSiteInfo();
    }

    private async Task<SiteInfo> GetSiteInfo()
    {
        var siteInfo = new SiteInfo();
        siteInfo.Badges = await _informationService.GetBadges();
        siteInfo.FotterSiteInfo = await _informationService.GetSiteInfo();
        siteInfo.Introduction = await _informationService.GetLeftInformation();
        siteInfo.NewBlogList = await _blogService.QueryNewBlog(5);
        siteInfo.RandomBlogList = await _blogService.QueryRandomBlogs(3);
        siteInfo.TagList = await _tagService.GetCacheListAsync();
        siteInfo.CategoryList = await _categoryService.GetCacheListAsync();
        return siteInfo;
    }

    public async Task<SiteInfo> RefreshSiteInfoAsync()
    {
        var siteInfo = await GetSiteInfo();
        if (_redis != null)
        {
            await _redis.StringSetAsync("blog:site", JsonConvert.SerializeObject(siteInfo));
        }

        return siteInfo;
    }
}