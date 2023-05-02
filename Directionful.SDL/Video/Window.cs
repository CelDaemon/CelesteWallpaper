using Directionful.SDL.Util;

namespace Directionful.SDL.Video;

public class Window : IDisposable
{
    private readonly nint _handle;
    private readonly string _title;
    private readonly Rectangle<int> _location;
    private readonly WindowFlag _flags;
    public Window(string title, Rectangle<int> location, WindowFlag flags)
    {
        _title = title;
        _location = location;
        _flags = flags;
        _handle = Native.SDL.Window.Create("test", location.X, location.Y, location.Width, location.Height, flags);
    }
    public void Dispose()
    {

    }
}