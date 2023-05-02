using Directionful.SDL.Event;
using Directionful.SDL.Util;

namespace Directionful.SDL.Video;

public class Window : IDisposable
{
    private readonly nint _handle;
    private string _title;
    private Rectangle<int> _location;
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
    public Rectangle<int> Location
    {
        get => _location;
        set
        {
            if(_disposed) throw new ObjectDisposedException(nameof(Window));
            if(_location == value) return;
            if(_location.X != value.X || _location.Y != value.Y)
            {
                Native.SDL.Window.SetPosition(_handle, value.X, value.Y);
            }
            if(_location.Width != value.Width || _location.Height != value.Height)
            {
                Native.SDL.Window.SetSize(_handle, value.Width, value.Height);
            }
            _location = value;
        }
    }
    public void Dispose()
    {
        if(_disposed) return;
        _disposed = true;
        GC.SuppressFinalize(this);
    }
}