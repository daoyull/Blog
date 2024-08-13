using Blog.Lib.Entity;

namespace Blog.Lib.Service;

public interface ISiteService
{
    Task<SiteInfo> GetSiteInfoAsync();

    Task<SiteInfo> RefreshSiteInfoAsync();
}