using Blog.Lib.Entity;
using Blog.Lib.Service;
using LanguageExt.Common;

namespace Blog.DbModule.Service.Impl;

public class InformationServiceImpl : IInformationService
{
    private readonly IConfigService _configService;

    public InformationServiceImpl(IConfigService configService)
    {
        _configService = configService;
    }

    public Task<Result<LeftInformation>> GetLeftInformation()
    {
        return _configService.GetJsonConfig<LeftInformation>("leftInformation");
    }

    public Task<Result<List<Badge>>> GetBadges()
    {
        return _configService.GetJsonConfig<List<Badge>>("badges");
    }

    public Task<Result<FotterSiteInfo>> GetSiteInfo()
    {
        return _configService.GetJsonConfig<FotterSiteInfo>("siteInfo");
    }
}
