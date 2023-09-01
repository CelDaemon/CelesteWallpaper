namespace Directionful.SDL;

public class SdlException : Exception
{
    public SdlException(string message) : base(Build(message)) { }

    private static string Build(string message)
    {
        var error = SdlNative.Error.Get();
        return $"{message}{(string.IsNullOrEmpty(error) ? string.Empty : $" ({error})")}";
    }
}