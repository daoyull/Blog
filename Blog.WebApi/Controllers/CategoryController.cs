using Blog.Lib.Models;
using Blog.Lib.Service;
using Microsoft.AspNetCore.Mvc;

namespace Blog.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet("/categorys")]
    public async Task<IActionResult> GetPageAsync([FromQuery] CategoryPageQueryDto query)
    {
        var pageAsync = await _categoryService.GetPageAsync(query);
        return pageAsync.HandleResult();
    }

    [HttpPost("/category")]
    public async Task<IActionResult> AddAsync([FromBody] CategoryAddDto category)
    {
        var addAsync = await _categoryService.AddAsync(category);
        return addAsync.HandleResult();
    }

    [HttpPut("/category")]
    public async Task<IActionResult> EditAsync([FromBody] CategoryEditDto category)
    {
        var editAsync = await _categoryService.EditAsync(category);
        return editAsync.HandleResult();
    }

    [HttpDelete("/category")]
    public async Task<IActionResult> EditAsync([FromQuery] long id)
    {
        var deleteAsync = await _categoryService.DeleteAsync(id);
        return deleteAsync.HandleResult();
    }

    [HttpGet("/category/all")]
    public async Task<IActionResult> GetAllTag()
    {
        var cacheListAsync = await _categoryService.GetCacheListAsync();
        return cacheListAsync.HandleResult();
    }
}