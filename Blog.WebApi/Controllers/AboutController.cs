using Blog.Lib.Service;
using Blog.WebApi.Aspect;
using Markdig;
using Microsoft.AspNetCore.Mvc;

namespace Blog.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class AboutController : ControllerBase
{
    private readonly IAboutMeService _aboutMeService;

    public AboutController(IAboutMeService aboutMeService)
    {
        _aboutMeService = aboutMeService;
    }

    [VisitLog(Behavitor = "关于我")]
    [HttpGet("/web/about")]
    public async Task<IActionResult> GetAbout()
    {
        var desc = await _aboutMeService.Desc();
        return  desc.HandleResult(d => R.Ok(Markdown.ToHtml(d)));
    }
}