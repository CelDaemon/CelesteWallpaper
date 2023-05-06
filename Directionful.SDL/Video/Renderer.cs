using Directionful.SDL.Native.Enum;

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
    private readonly nint _handle;
    private bool _disposed;
}