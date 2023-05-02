using Directionful.SDL.Event;
using Directionful.SDL.Util;

namespace Directionful.SDL.Video;

public class Window : IDisposable
{
    private readonly Video.UnregisterWindow _unregisterHandler;
    private readonly nint _handle;
    private readonly uint _id;
    private string _title;
    private Rectangle<int> _location;
    private bool _disposed;
    internal Window(Video.UnregisterWindow unregisterHandler, string title, Rectangle<int> location, WindowFlag flags)
    {
        _unregisterHandler = unregisterHandler;
        _title = title;
        _location = location;
        _handle = Native.SDL.Window.Create("test", location.X, location.Y, location.Width, location.Height, flags);
        _id = Native.SDL.Window.GetID(_handle);
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
    public uint ID {get => _id; }
    public void HandleEvent(WindowEvent evt)
    {
        if(_disposed) throw new ObjectDisposedException(nameof(Window));
        switch(evt.Event)
        {
            case WindowEventType.Moved:
                _location = _location with {X = evt.Data1, Y = evt.Data2};
                break;
            case WindowEventType.Resized:
            case WindowEventType.SizeChanged:
                _location = _location with {Width = evt.Data1, Height = evt.Data2};
                break;
        }
    }
    public void Dispose()
    {
        if(_disposed) return;
        _disposed = true;
        GC.SuppressFinalize(this);
        _unregisterHandler.Invoke(ID);
    }
}