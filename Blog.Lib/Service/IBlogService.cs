using Blog.Lib.Entity;
using Blog.Lib.Models;
using Common.Lib.Attributes;
using Common.Lib.Models;
using LanguageExt.Common;

namespace Blog.Lib.Service;

public interface IBlogService
{
    /// <summary>
    /// 获取分页博客
    /// </summary>
    /// <param name="query"></param>
    /// <param name="showPass">显示密码</param>
    /// <returns></returns>
    Task<Result<PageResult<BlogVo>>> GetPageAsync(BlogPageQueryDto query,bool showPass = false);


    /// <summary>
    /// 查询卡片需要的数据
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    Task<Result<PageResult<BlogCardVo>>> QueryCardPageList(BlogPageQueryDto query);

    /// <summary>
    /// 查询归档数据
    /// </summary>
    /// <returns></returns>
    Task<Result<List<Archive>>> QueryArchiveList();

    /// <summary>
    /// 查询博客详细数据
    /// </summary>
    /// <param name="blogId">博客Id</param>
    /// <param name="showPass">是否显示密码</param>
    /// <returns></returns>
    Task<Result<BlogCardVo>> GetBlogContentAsync(long blogId,bool showPass = false);

    /// <summary>
    /// 查询标签下的博客
    /// </summary>
    /// <param name="tagId">标签id</param>
    /// <param name="pageNum"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    Task<Result<PageResult<BlogCardVo>>> GetBlogPageByTagId(long tagId, int pageNum, int pageSize);

    /// <summary>
    /// 查询分类下的博客
    /// </summary>
    /// <param name="categoryId">分类Id</param>
    /// <param name="pageNum"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    Task<Result<PageResult<BlogCardVo>>> GetBlogPageByCategoryId(long categoryId, int pageNum, int pageSize);

    /// <summary>
    /// 查询最新的博客
    /// </summary>
    /// <param name="num"></param>
    /// <returns></returns>
    Task<Result<List<NewBlog>>> QueryNewBlog(int num);


    /// <summary>
    /// 查询随机博客
    /// </summary>
    /// <param name="num"></param>
    /// <returns></returns>
    Task<Result<List<RandomBlog>>> QueryRandomBlogs(int num);

    [DataVerify]
    Task<Result<int>> AddAsync(BlogAddDto model);

    [DataVerify]
    Task<Result<int>> EditAsync(BlogEditDto model);

    Task<Result<int>> DeleteAsync(long argId);
    
    Task<Result<int>> DeleteListAsync(List<long> toList);
    Task<Result<List<SearchBlogResult>>> GetSearchBlog(string query);
    Task<Result<bool>> TopAsync(long id, bool top);
    Task<Result<bool>> RecommendAsync(long id, bool recommend);
    Task<Result<bool>> EditVisibility(long id,BlogEditVisibilityDto blog);
}