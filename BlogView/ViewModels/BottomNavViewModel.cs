using Blog.Lib.Entity;
using Blog.Lib.Service;
using BlogView.Service;

using Common.Lib.Service;
using Common.Mvvm.Abstracts;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace BlogView.ViewModels;

public partial class BottomNavViewModel : BaseViewModel,IRefresh
{
    private readonly IBlogService _blogService;
    private readonly ILogger<BottomNavViewModel> _logger;
    private readonly IConfigService _configService;
    public PageService PageService { get; }
    [ObservableProperty] private List<NewBlog>? _newBlogs;
    [ObservableProperty] private List<Badge>? _badges;

    [ObservableProperty] private string? _hitokoto = "    我们生活的每个日常，都是连续发生的奇迹";
    [ObservableProperty] private string? _from = "—— 《日常》";

    public BottomNavViewModel(IBlogService blogService, ILogger<BottomNavViewModel> logger, PageService pageService,
        IConfigService configService)
    {
        _blogService = blogService;
        _logger = logger;
        _configService = configService;
        PageService = pageService;
        
    }
    
    public  async Task Refresh()
    {
        using (HttpClient client = new HttpClient())
        {
            try
            {
                var resStr = await client.GetStringAsync("https://v1.hitokoto.cn/?c=a");
                dynamic res = JsonConvert.DeserializeObject(resStr)!;
                Hitokoto = $"    {res.hitokoto}";
                From = $"—— 《{res.from}》";
            }
            catch (Exception e)
            {
                _logger.LogError(e, "获取每日一言失败");
            }
        }
        NewBlogs = null;
        Badges = null;
        NewBlogs = await _blogService.QueryNewBlog(3);
        Badges = await _configService.GetJsonConfig<List<Badge>>("badges");
        
        
    }
}
