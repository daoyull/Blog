

namespace Blog.Lib.Service;

public interface IAboutMeService
{
    /// <summary>
    /// 查询关于我的说明
    /// </summary>
    /// <returns></returns>
    Task<string> Desc();
}
