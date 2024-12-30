namespace BlogView.Helpers;

public static class ResultHelper
{
    public static Task<object?> HandleException(Exception e)
    {
        return default;
    }

    public static void Handle<T>(this T result, Action<T> action)
    {
        action.Invoke(result);
    }
}