using Blog.Lib.Models;
using Blog.Lib.Service;
using Blog.WebApi.Aspect;
using Markdig;
using Microsoft.AspNetCore.Mvc;

namespace Blog.WebApi.Controllers;

/// <summary>
/// Web站点用的接口
/// </summary>
[ApiController]
[Route("[controller]")]
public partial class BlogController : ControllerBase
{
    private readonly IBlogService _blogService;
    private readonly ICategoryService _categoryService;
    private readonly ITagService _tagService;

    public BlogController(IBlogService blogService, ICategoryService categoryService, ITagService tagService)
    {
        _blogService = blogService;
        _categoryService = categoryService;
        _tagService = tagService;
    }

    [VisitLog(Behavitor = "文章列表")]
    [HttpGet("/web/blogs")]
    public async Task<IActionResult> Blogs([FromQuery] BlogPageQueryDto query)
    {
        var ResultPage = await _blogService.GetPageAsync(query);
        ResultPage.IfSucc(it => it.List.ForEach(i => i.Description = Markdown.ToHtml(i.Description ?? "")));
        return ResultPage.HandleResult();
    }

    [VisitLog(Behavitor = "文章详情")]
    [HttpGet("/web/blog")]
    public async Task<IActionResult> Blog([FromQuery] long id)
    {
        var blogResult = await _blogService.GetBlogContentAsync(id);
        blogResult.IfSucc(i => i.Content = Markdown.ToHtml(i.Content ?? ""));
        return blogResult.HandleResult();
    }

    [VisitLog(Behavitor = "分类文章列表")]
    [HttpGet("/web/blog/getBlogByCategoryName")]
    public async Task<IActionResult> GetByCategoryName([FromQuery] string categoryName, [FromQuery] int pageNum = 1,
        [FromQuery] int pageSize = 10)
    {
        var result = await _categoryService.GetIdByNameAsync(categoryName);
        return await result.Match<Task<IActionResult>>(async id =>
        {
            var blog = await _blogService.GetBlogPageByCategoryId(id, pageNum, pageSize);
            blog.IfSucc(it => it.List.ForEach(i => i.Description = Markdown.ToHtml(i.Description ?? "")));
            return blog.HandleResult();
        }, ex =>
        {
            var handleException = HandleException(ex);
            return Task.FromResult(handleException);
        });
    }

    [VisitLog(Behavitor = "搜索文章")]
    [HttpGet("/web/searchBlog")]
    public async Task<IActionResult> SearchBlog([FromQuery] string query)
    {
        var searchBlogResults = await _blogService.GetSearchBlog(query);
        return searchBlogResults.HandleResult();
    }

    [VisitLog(Behavitor = "标签文章列表")]
    [HttpGet("/web/blog/getBlogByTagName")]
    public async Task<IActionResult> GetByTagName([FromQuery] string tagName, [FromQuery] int pageNum = 1,
        [FromQuery] int pageSize = 10)
    {
        var idResult = await _tagService.GetIdByNameAsync(tagName);

        return await idResult.Match<Task<IActionResult>>(async id =>
        {
            var blog = await _blogService.GetBlogPageByTagId(id, pageNum, pageSize);
            blog.IfSucc(it => it.List.ForEach(i => i.Description = Markdown.ToHtml(i.Description ?? "")));
            return blog.HandleResult();
        }, exception => Task.FromIActionResult>(Ok(R.Fail(exception.Message))));
    }

    [VisitLog(Behavitor = "归档")]
    [HttpGet("/web/blog/getArchiveList")]
    public async Task<IActionResult> QueryArchiveList()
    {
        var queryArchiveList = await _blogService.QueryArchiveList();
        return queryArchiveList.HandleResult(list =>
        {
            var count = list.SelectMany(it => it.Blogs).Count();
            return R.Ok(new
            {
                count, list
            });
        });
    }

    [HttpPost("/admin/blog")]
    public async Task<IActionResult> AddSync([FromBody] BlogAddDto blog)
    {
        var addAsync = await _blogService.AddAsync(blog);
        return addAsync.HandleResult();
    }

    [HttpPut("/admin/blog")]
    public async Task<IActionResult> EditSync([FromBody] BlogEditDto blog)
    {
        var editAsync = await _blogService.EditAsync(blog);
        return editAsync.HandleResult();
    }

    [HttpGet("/admin/blogs")]
    public async Task<IActionResult> GetPageAsync([FromQuery] BlogPageQueryDto query)
    {
        var result = await _blogService.GetPageAsync(query, true);
        return result.HandleResult();
    }

    [HttpPut("/admin/blog/top")]
    public async Task<IActionResult> Top([FromQuery] long id, [FromQuery] bool top)
    {
        var topAsync = await _blogService.TopAsync(id, top);
        return topAsync.HandleResult();
    }

    [HttpPut("/admin/blog/recommend")]
    public async Task<IActionResult> Recommend([FromQuery] long id, [FromQuery] bool recommend)
    {
        var recommendAsync = await _blogService.RecommendAsync(id, recommend);
        return recommendAsync.HandleResult();
    }

    [HttpPut("/admin/blog/visibility/{id}")]
    public async Task<IActionResult> EditVisibility([FromRoute] long id, [FromBody] BlogEditVisibilityDto blog)
    {
        var editVisibility = await _blogService.EditVisibility(id, blog);
        return editVisibility.HandleResult();
    }

    [HttpGet("/admin/blog")]
    public async Task<IActionResult> GetAsync([FromQuery] long id)
    {
        var result = await _blogService.GetBlogContentAsync(id, true);
        return result.HandleResult();
    }

    [HttpDelete("/admin/blog")]
    public async Task<IActionResult> Delete([FromQuery] long id)
    {
        var deleteAsync = await _blogService.DeleteAsync(id);
        return deleteAsync.HandleResult();
    }
}