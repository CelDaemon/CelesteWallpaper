using Directionful.SDL.Util;

namespace Directionful.SDL.Video;

public class Renderer : IDisposable
{
    private readonly nint _handle;
    private bool _disposed;
    internal Renderer(nint windowHandle, RenderFlag flags)
    {
        _handle = Native.SDL.Renderer.Create(windowHandle, -1, flags);
    }
    public void Clear(Color color)
    {
        Native.SDL.Renderer.SetDrawColor(_handle, color);
        Native.SDL.Renderer.Clear(_handle);
    }
    public void Present()
    {
        Native.SDL.Renderer.Present(_handle);
    }
    public void Dispose()
    {
        if(_disposed) return;
        _disposed = true;
        GC.SuppressFinalize(this);
        Native.SDL.Renderer.Destroy(_handle);
    }
}