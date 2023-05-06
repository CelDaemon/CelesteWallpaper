using Directionful.SDL.Native.Flag;
using Directionful.SDL.Util;

namespace Directionful.SDL.Video.Windowing;

public class Window : IDisposable
{
    public Window(string title, Rectangle<int> location, bool resizable)
    {
        var flags = WindowFlag.None;
        if(resizable) flags |= WindowFlag.Resizable;
        _handle = Native.SDL.Window.Create(title, location, flags);
    }
    public void Dispose()
    {
        if(_disposed) return;
        _disposed = true;
        GC.SuppressFinalize(this);
        Native.SDL.Window.Destroy(_handle);
    }

    private readonly nint _handle;
    private bool _disposed;
}