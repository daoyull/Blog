using LanguageExt.Common;
using Microsoft.AspNetCore.Mvc;

namespace Blog.WebApi.Helpers;

public static class ResultHelper
{
    public static IActionResult HandleException(Exception e)
    {
        return new OkObjectResult(R.Fail(e.Message));
    }

    public static IActionResult HandleResult<T>(this Result<T> result, Func<T, R>? action = null)
    {
        var match = result.Match(t =>
        {
            R r ;
            if (action != null)
            {
                r = action(t);
            }
            else
            {
                r = R.Ok(t);
            }

            return new OkObjectResult(r);
        }, HandleException);
        return match;
    }
}