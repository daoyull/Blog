using Common.Avalonia.Service;
using Common.Lib.Ioc;
using LanguageExt.Common;

namespace BlogBackManager.Helpers;

public static class ResultHelper
{
    public static Task<object?> HandleException(Exception e)
    {
        var globalNotify = Ioc.Resolve<IGlobalNotify>();
        globalNotify.Error("错误", e.Message);
        return Task.FromResult<object?>(default);
    }

    public static void Handle<T>(this Result<T> result, Action<T> action)
    {
        var match = result.Match<object?>(r =>
        {
            action(r);
            return default;
        }, HandleException);
    }
}