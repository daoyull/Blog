using Blog.Lib.Entity;
using Blog.Lib.Models;
using Common.Lib.Attributes;
using Common.Lib.Models;

namespace Blog.Lib.Service;

public interface IBlogService
{
    /// <summary>
    /// 获取分页博客
    /// </summary>
    /// <param name="query"></param>
    /// <param name="showPass">显示密码</param>
    /// <returns></returns>
    Task<PageResult<BlogVo>> GetPageAsync(BlogPageQueryDto query, bool showPass = false);


    /// <summary>
    /// 查询卡片需要的数据
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    Task<PageResult<BlogCardVo>> QueryCardPageList(BlogPageQueryDto query);

    /// <summary>
    /// 查询归档数据
    /// </summary>
    /// <returns></returns>
    Task<List<Archive>> QueryArchiveList();

    /// <summary>
    /// 查询博客详细数据
    /// </summary>
    /// <param name="blogId">博客Id</param>
    /// <param name="showPass">是否显示密码</param>
    /// <returns></returns>
    Task<BlogCardVo> GetBlogContentAsync(long blogId, bool showPass = false);

    /// <summary>
    /// 查询标签下的博客
    /// </summary>
    /// <param name="tagId">标签id</param>
    /// <param name="pageNum"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    Task<PageResult<BlogCardVo>> GetBlogPageByTagId(long tagId, int pageNum, int pageSize);

    /// <summary>
    /// 查询分类下的博客
    /// </summary>
    /// <param name="categoryId">分类Id</param>
    /// <param name="pageNum"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    Task<PageResult<BlogCardVo>> GetBlogPageByCategoryId(long categoryId, int pageNum, int pageSize);

    /// <summary>
    /// 查询最新的博客
    /// </summary>
    /// <param name="num"></param>
    /// <returns></returns>
    Task<List<NewBlog>> QueryNewBlog(int num);


    /// <summary>
    /// 查询随机博客
    /// </summary>
    /// <param name="num"></param>
    /// <returns></returns>
    Task<List<RandomBlog>> QueryRandomBlogs(int num);

    Task<int> AddAsync(BlogAddDto model);


    Task<int> EditAsync(BlogEditDto model);

    Task<int> DeleteAsync(long argId);

    Task<int> DeleteListAsync(List<long> toList);
    Task<List<SearchBlogResult>> GetSearchBlog(string query);
    Task<bool> TopAsync(long id, bool top);
    Task<bool> RecommendAsync(long id, bool recommend);
    Task<bool> EditVisibility(long id, BlogEditVisibilityDto blog);
}