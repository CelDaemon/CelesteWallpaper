using Directionful.SDL.Video.Windowing;

namespace Directionful.SDL.Video;

public class VideoSystem : IDisposable
{
    public void Dispose()
    {
        if (_disposed) return;
        _disposed = true;
        GC.SuppressFinalize(this);
    }
    internal VideoSystem() { }
    internal void RegisterWindow(Window window)
    {
        _windows.Add(window.Id, window);
    }
    internal void UnregisterWindow(Window window) => _windows.Remove(window.Id);
    internal Window GetWindow(uint id) => _windows[id];
    private readonly Dictionary<uint, Window> _windows = new();
    private bool _disposed;
}