using Microsoft.AspNetCore.Mvc.Filters;

namespace Blog.WebApi.Aspect;

/// <summary>
/// 权限验证
/// </summary>
public class RoleAttribute : ActionFilterAttribute
{
    public RoleAttribute()
    {
    }

    /// <summary>
    /// 是否需要Jwt认证
    /// </summary>
    public bool JwtAuth { get; set; } = false;

    /// <summary>
    /// 异步执行时
    /// </summary>
    /// <param name="context">数据上下文</param>
    /// <param name="next">ActionExecutionDelegate</param>
    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        // 继续执行
        await base.OnActionExecutionAsync(context, next);
    }
}