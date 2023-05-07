using Directionful.SDL.Native.Enum;
using Directionful.SDL.Util;

namespace Directionful.SDL.Video;

public class Renderer : IDisposable
{
    public void Dispose()
    {
        if(_disposed) return;
        _disposed = true;
        GC.SuppressFinalize(this);
        Native.SDL.Renderer.Destroy(_handle);
    }
    internal Renderer(nint window, int index, bool vsync = false, bool software = false, bool targetTexture = false)
    {
        var flags = RendererFlag.None;
        if(vsync) flags |= RendererFlag.PresentVSync;
        if (software) flags |= RendererFlag.Software;
        else flags |= RendererFlag.Accelerated;
        if (targetTexture) flags |= RendererFlag.TargetTexture;
        _handle = Native.SDL.Renderer.Create(window, index, flags);
    }
    public void DrawRectangle(Rectangle<float> rect, Color color, bool filled = true)
    {
        if(_disposed) throw new ObjectDisposedException(nameof(Renderer));
        Native.SDL.Renderer.SetDrawColor(_handle, color);
        if(filled) Native.SDL.Renderer.FillRectangle(_handle, rect);
        else Native.SDL.Renderer.DrawRectangle(_handle, rect);
    }
    public void Clear(Color color)
    {
        if(_disposed) throw new ObjectDisposedException(nameof(Renderer));
        Native.SDL.Renderer.SetDrawColor(_handle, color);
        Native.SDL.Renderer.Clear(_handle);
    }
    public void Present()
    {
        if(_disposed) throw new ObjectDisposedException(nameof(Renderer));
        Native.SDL.Renderer.Present(_handle);
    }
    private readonly nint _handle;
    private bool _disposed;
}