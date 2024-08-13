using Blog.Lib.Models;
using Blog.Lib.Service;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace Blog.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("/admin/user/login")]
    public async Task<IActionResult> Login(UserLoginDto user)
    {
        var result = await _userService.Login(user);
        return result.HandleResult();
    }
}