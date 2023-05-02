using Directionful.SDL.Util;

namespace Directionful.SDL.Video;

public class Window : IDisposable
{
    private readonly nint _handle;
    private readonly Rectangle<int> _location;
    private string _title;
    private bool _disposed;
    public Window(string title, Rectangle<int> location, WindowFlag flags)
    {
        _title = title;
        _location = location;
        _handle = Native.SDL.Window.Create("test", location.X, location.Y, location.Width, location.Height, flags);
    }
    public string Title 
    {
        get => _title;
        set
        {
            if(_disposed) throw new ObjectDisposedException(nameof(Window));
            if(_title == value) return;
            Native.SDL.Window.SetTitle(_handle, value);
            _title = value;
        }
    }
    public void Dispose()
    {
        if(_disposed) return;
        _disposed = true;
        GC.SuppressFinalize(this);
    }
}