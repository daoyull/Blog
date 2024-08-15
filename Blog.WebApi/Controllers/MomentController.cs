using Blog.Lib.Models;
using Blog.Lib.Service;
using Blog.WebApi.Aspect;
using Markdig;
using Microsoft.AspNetCore.Mvc;

namespace Blog.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public partial class MomentController : ControllerBase
{
    private readonly IMomentService _momentService;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="momentService">动态服务</param>
    public MomentController(IMomentService momentService)
    {
        _momentService = momentService;
    }


    [VisitLog(Behavitor = "动态")]
    [HttpGet("/web/moments")]
    public async Task<IActionResult> QueryPageListFront([FromQuery] MomentPageQueryDto query)
    {
        var momentResult = await _momentService.GetMomentPageList(query);
        momentResult.IfSucc(s => s.List.ForEach(it => it.Content = Markdown.ToHtml(it.Content ?? "")));
        return momentResult.HandleResult();
    }

    [HttpPut("/web/moment/like/{id}")]
    public async Task<IActionResult> LikeMoment([FromRoute] long id)
    {
        var likeMoment = await _momentService.LikeMoment(id);
        return likeMoment.HandleResult();
    }

    [HttpGet("/admin/moments")]
    public async Task<IActionResult> QueryAdminPageListFront([FromQuery] MomentPageQueryDto query)
    {
        var momentPageListVo = await _momentService.GetMomentPageList(query);
        return momentPageListVo.HandleResult();
    }

    [HttpPut("/admin/moment/published")]
    public async Task<IActionResult> Published([FromQuery] long id, [FromQuery] bool published)
    {
        var result = await _momentService.Published(id, published);
        return result.HandleResult();
    }

    [HttpGet("/admin/moment")]
    public async Task<IActionResult> GetAsync([FromQuery] long id)
    {
        var vo = await _momentService.GetAsync(id);
        return vo.HandleResult();
    }

    [HttpPut("/admin/moment")]
    public async Task<IActionResult> EditAsync([FromBody] MomentEditDto moment)
    {
        var editAsync = await _momentService.EditAsync(moment);
        return editAsync.HandleResult();
    }

    [HttpPost("/admin/moment")]
    public async Task<IActionResult> EditAsync([FromBody] MomentAddDto moment)
    {
        var addAsync = await _momentService.AddAsync(moment);
        return addAsync.HandleResult();
    }

    [HttpDelete("/admin/moment")]
    public async Task<IActionResult> Delete([FromQuery] long id)
    {
        var deleteAsync = await _momentService.DeleteAsync(id);
        return deleteAsync.HandleResult();
    }
}