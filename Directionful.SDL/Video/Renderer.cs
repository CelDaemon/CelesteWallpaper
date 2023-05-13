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
    public void DrawRectangle(Rectangle<float> rect, Color color, BlendMode blendMode = BlendMode.None, bool filled = true)
    {
        if(_disposed) throw new ObjectDisposedException(nameof(Renderer));
        DrawColor = color;
        DrawBlendMode = blendMode;
        if(filled) Native.SDL.Renderer.FillRectangle(_handle, rect);
        else Native.SDL.Renderer.DrawRectangle(_handle, rect);
    }
    public void Clear(Color color)
    {
        if(_disposed) throw new ObjectDisposedException(nameof(Renderer));
        DrawColor = color;
        Native.SDL.Renderer.Clear(_handle);
    }
    public void Present()
    {
        if(_disposed) throw new ObjectDisposedException(nameof(Renderer));
        Native.SDL.Renderer.Present(_handle);
    }
    private readonly nint _handle;
    private bool _disposed;
    private Color _drawColor;
    private BlendMode _drawBlendMode;
    private Color DrawColor
    {
        get => _drawColor;
        set
        {
            if(_drawColor == value) return;
            Native.SDL.Renderer.SetDrawColor(_handle, value);
            _drawColor = value;
        }
    }
    private BlendMode DrawBlendMode
    {
        get => _drawBlendMode;
        set
        {
            if(_drawBlendMode == value) return;
            Native.SDL.Renderer.SetDrawBlendMode(_handle, value);
            _drawBlendMode = value;
        }
    }
}