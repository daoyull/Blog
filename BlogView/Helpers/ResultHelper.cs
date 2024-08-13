using LanguageExt.Common;

namespace BlogView.Helpers;

public static class ResultHelper
{
    public static Task<object?> HandleException(Exception e)
    {
        return default;
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