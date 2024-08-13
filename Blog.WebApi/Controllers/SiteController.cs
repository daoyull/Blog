using Blog.Lib.Service;
using Blog.WebApi.Aspect;
using Microsoft.AspNetCore.Mvc;

namespace Blog.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public partial class SiteController : ControllerBase
{
    private readonly ISiteService _siteService;

    public SiteController(ISiteService siteService)
    {
        _siteService = siteService;
    }

    [VisitLog(Behavitor = "SiteInfo")]
    [HttpGet("/web/site")]
    public async Task<IActionResult> Site()
    {
        var siteInfoAsync = await _siteService.GetSiteInfoAsync();
        return Ok(R.Ok(siteInfoAsync));
    }
}