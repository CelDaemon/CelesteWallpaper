namespace Directionful.SDL;

public class SDLException : Exception
{
    public SDLException(string message) : base(Build(message)) { }

    private static string Build(string message)
    {
        var error = Native.SDL.Error.Get();
        return $"{message}{(string.IsNullOrEmpty(error) ? string.Empty : $" ({error})")}";
    }
}