using Blog.Lib.Models;
using Blog.Lib.Service;
using Blog.WebApi.Aspect;
using Microsoft.AspNetCore.Mvc;

namespace Blog.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class TagController : ControllerBase
{
    private readonly ITagService _tagService;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="tagService">标签服务</param>
    public TagController(ITagService tagService)
    {
        _tagService = tagService;
    }


    [HttpGet("/tags")]
    public async Task<IActionResult> GetPageAsync([FromQuery] TagPageQueryDto query)
    {
        var pageAsync = await _tagService.GetPageAsync(query);
        return  pageAsync.HandleResult();
    }

    [HttpPost("/tag")]
    public async Task<IActionResult> AddAsync([FromBody] TagAddDto tag)
    {
        var addAsync = await _tagService.AddAsync(tag);
        return addAsync.HandleResult();
    }

    [HttpPut("/tag")]
    public async Task<IActionResult> EditAsync([FromBody] TagEditDto tag)
    {
        var editAsync = await _tagService.EditAsync(tag);
        return editAsync.HandleResult();
    }

    [HttpDelete("/tag")]
    public async Task<IActionResult> EditAsync([FromQuery] long id)
    {
        var deleteAsync = await _tagService.DeleteAsync(id);
        return deleteAsync.HandleResult();
    }

    [HttpGet("/tag/all")]
    public async Task<IActionResult> GetAllTag()
    {
        var cacheListAsync = await _tagService.GetCacheListAsync();
        return cacheListAsync.HandleResult();
    }
}