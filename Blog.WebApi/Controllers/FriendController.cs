using Blog.DbModule.Models;
using Blog.Lib.Models;
using Blog.Lib.Service;
using Blog.WebApi.Aspect;
using Markdig;
using Microsoft.AspNetCore.Mvc;

namespace Blog.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public partial class FriendController : ControllerBase
{
    private readonly IFriendService _friendService;
    private readonly IBlogService _blogService;

    public FriendController(IFriendService friendService, IBlogService blogService)
    {
        _friendService = friendService;
        _blogService = blogService;
    }

    [VisitLog(Behavitor = "友链")]
    [HttpGet("/web/friends")]
    public async Task<IActionResult> GetFriends()
    {
        var list = await _friendService.GetFriendVoList(12);
        return list.HandleResult();
    }
    
    [HttpGet("/web/friend/desc")]
    public async Task<IActionResult> GetDesc()
    {
        var desc = await _friendService.GetDesc();
        return desc.HandleResult(vo => R.Ok(data: Markdown.ToHtml(vo.Content ?? "")));
    }

    [HttpGet("/admin/friends")]
    public async Task<IActionResult> GetPageList([FromQuery] FriendQueryDto friend)
    {
        var result = await _friendService.GetFriendPage(friend);
        return result.HandleResult();
    }

    [HttpPut("/admin/friend/published")]
    public async Task<IActionResult> Published([FromQuery] long id, [FromQuery] bool published)
    {
        var result = await _friendService.Published(id, published);
        return result.HandleResult();
    }

    [HttpPost("/admin/friend")]
    public async Task<IActionResult> AddFriend([FromBody] FriendAddDto friend)
    {
        var result = await _friendService.AddAsync(friend);
        return result.HandleResult();
    }

    [HttpPut("/admin/friend")]
    public async Task<IActionResult> EditFriend([FromBody] FriendEditDto friend)
    {
        var result = await _friendService.EditAsync(friend);
        return result.HandleResult();
    }

    [HttpGet("/admin/friendInfo")]
    public async Task<IActionResult> FriendInfo()
    {
        var result = await _friendService.GetDesc();
        return result.HandleResult();
    }

    [HttpPut("/admin/friendInfo/content")]
    public async Task<IActionResult> FriendInfoContent([FromBody] FriendContentDto content)
    {
        var result = await _friendService.EditFriendInfoContent(content.Content ?? "");
        return result.HandleResult();
    }

    [HttpPut("/admin/friendInfo/commentEnabled")]
    public async Task<IActionResult> EditCommentEnabled([FromQuery] bool commentEnabled)
    {
        var result = await _friendService.EditCommentEnabled(commentEnabled);
        return result.HandleResult();
    }
}