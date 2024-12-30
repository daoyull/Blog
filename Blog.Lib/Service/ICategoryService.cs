using Blog.Lib.Models;
using Common.Lib.Attributes;
using Common.Lib.Models;


namespace Blog.Lib.Service;

public interface ICategoryService
{
    /// <summary>
    /// 查询分类
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    public Task<CategoryVo> GetAsync(CategoryQueryDto query);

    /// <summary>
    /// 查询分类分页数据
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    public Task<PageResult<CategoryVo>> GetPageAsync(CategoryPageQueryDto query);

    /// <summary>
    /// 新增分类
    /// </summary>
    /// <param name="category"></param>
    /// <returns></returns>
   
    public Task<int> AddAsync(CategoryAddDto category);

    /// <summary>
    /// 修改分类
    /// </summary>
    /// <param name="category"></param>
    /// <returns></returns>
   
    public Task<int> EditAsync(CategoryEditDto category);

    /// <summary>
    /// 删除分类
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Task<int> DeleteAsync(long id);

    /// <summary>
    /// 删除分类列表
    /// </summary>
    /// <param name="ids"></param>
    /// <returns></returns>
    public Task<int> DeleteListAsync(List<long> ids);

    /// <summary>
    /// 获取缓存分类列表
    /// </summary>
    /// <returns></returns>
    public Task<List<CategoryCacheVo>> GetCacheListAsync();

    Task<List<CountChart>> GetTagBlogNum();
    Task<long> GetIdByNameAsync(string categoryName);
}