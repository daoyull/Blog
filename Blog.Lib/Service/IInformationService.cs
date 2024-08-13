using Blog.Lib.Entity;
using LanguageExt.Common;


namespace Blog.Lib.Service;

public interface IInformationService
{
    /// <summary>
    /// 查询左侧的基础信息
    /// </summary>
    /// <returns></returns>
    Task<Result<LeftInformation>> GetLeftInformation();

    /// <summary>
    /// 查询尾部标志
    /// </summary>
    /// <returns></returns>
    Task<Result<List<Badge>>> GetBadges();

    /// <summary>
    /// 查询SiteInfo
    /// </summary>
    /// <returns></returns>
    Task<Result<FotterSiteInfo>> GetSiteInfo();
}
