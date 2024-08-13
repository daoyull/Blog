using Blog.Lib.Models;
using Common.Lib.Attributes;
using Common.Lib.Models;
using LanguageExt.Common;

namespace Blog.Lib.Service;

public interface ITagService
{
    /// <summary>
    /// 查询标签
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    public Task<Result<TagVo>> GetAsync(TagQueryDto query);

    /// <summary>
    /// 查询标签分页数据
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    public Task<Result<PageResult<TagVo>>> GetPageAsync(TagPageQueryDto query);

    /// <summary>
    /// 新增标签
    /// </summary>
    /// <param name="tag"></param>
    /// <returns></returns>
    [DataVerify]
    public Task<Result<int>> AddAsync(TagAddDto tag);

    /// <summary>
    /// 修改标签
    /// </summary>
    /// <param name="tag"></param>
    /// <returns></returns>
    [DataVerify]
    public Task<Result<int>> EditAsync(TagEditDto tag);

    /// <summary>
    /// 删除标签
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Task<Result<int>> DeleteAsync(long id);

    /// <summary>
    /// 删除标签列表
    /// </summary>
    /// <param name="ids"></param>
    /// <returns></returns>
    public Task<Result<int>> DeleteListAsync(List<long> ids);

    /// <summary>
    /// 获取缓存标签列表
    /// </summary>
    /// <returns></returns>
    public Task<Result<List<TagCacheVo>>> GetCacheListAsync();
    
    /// <summary>
    /// 查询标签总数图数据
    /// </summary>
    /// <returns></returns>
    ValueTask<Result<List<CountChart>>> GetTagBlogNum();

    Task<Result<long>> GetIdByNameAsync(string tagName);
}