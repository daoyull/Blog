using Blog.Lib.Models;
using Common.Lib.Attributes;
using Common.Lib.Models;
using LanguageExt.Common;

namespace Blog.Lib.Service;

public interface ICategoryService
{
    /// <summary>
    /// 查询分类
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    public Task<Result<CategoryVo>> GetAsync(CategoryQueryDto query);

    /// <summary>
    /// 查询分类分页数据
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    public Task<Result<PageResult<CategoryVo>>> GetPageAsync(CategoryPageQueryDto query);

    /// <summary>
    /// 新增分类
    /// </summary>
    /// <param name="category"></param>
    /// <returns></returns>
    [DataVerify]
    public Task<Result<int>> AddAsync(CategoryAddDto category);

    /// <summary>
    /// 修改分类
    /// </summary>
    /// <param name="category"></param>
    /// <returns></returns>
    [DataVerify]
    public Task<Result<int>> EditAsync(CategoryEditDto category);

    /// <summary>
    /// 删除分类
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Task<Result<int>> DeleteAsync(long id);

    /// <summary>
    /// 删除分类列表
    /// </summary>
    /// <param name="ids"></param>
    /// <returns></returns>
    public Task<Result<int>> DeleteListAsync(List<long> ids);

    /// <summary>
    /// 获取缓存分类列表
    /// </summary>
    /// <returns></returns>
    public Task<Result<List<CategoryCacheVo>>> GetCacheListAsync();

    Task<Result<List<CountChart>>> GetTagBlogNum();
    Task<Result<long>> GetIdByNameAsync(string categoryName);
}