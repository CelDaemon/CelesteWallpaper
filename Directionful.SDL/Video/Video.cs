using Directionful.SDL.Util;

namespace Directionful.SDL.Video;

public class Video : IDisposable
{
    internal delegate void UnregisterWindow(uint id);
    private bool _disposed;
    public Dictionary<uint, Window> _windows = new();

    public Window CreateWindow(string title, Rectangle<int> location, WindowFlag flags)
    {
        var window = new Window(id => _windows.Remove(id), title, location, flags);
        _windows[window.ID] = window;
        return window;
    }

    internal Window GetWindow(uint id) => _windows[id];

    public void Dispose()
    {
        if(_disposed) return;
        _disposed = true;
        GC.SuppressFinalize(this);
        foreach(var window in _windows.Values)
        {
            window.Dispose();
        }
    }
}