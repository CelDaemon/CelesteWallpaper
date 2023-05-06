using Directionful.SDL.Native.Flag;
using Directionful.SDL.Util;

namespace Directionful.SDL.Video.Windowing;

public class Window : IDisposable
{
    public Window(string title, Rectangle<int> location, bool resizable = true, bool borderless = false)
    {
        var flags = WindowFlag.None;
        _resizable = resizable;
        _borderless = borderless;
        if (resizable) flags |= WindowFlag.Resizable;
        if (borderless) flags |= WindowFlag.Borderless;
        _handle = Native.SDL.Window.Create(title, location, flags);
    }
    public void Dispose()
    {
        if (_disposed) return;
        _disposed = true;
        GC.SuppressFinalize(this);
        Native.SDL.Window.Destroy(_handle);
    }
    public bool Resizable
    {
        get => _resizable;
        set
        {
            if (_disposed) throw new ObjectDisposedException(nameof(Window));
            if (_resizable == value) return;
            Native.SDL.Window.SetResizable(_handle, value);
            _resizable = value;
        }
    }

    private readonly nint _handle;
    private bool _disposed;
    private bool _resizable;
    private bool _borderless;
}