using Directionful.SDL.Enum;
using Directionful.SDL.Util;

namespace Directionful.SDL.Video;

public class Renderer : IDisposable
{
    public void Dispose()
    {
        if(_disposed) return;
        _disposed = true;
        GC.SuppressFinalize(this);
        SdlNative.Renderer.Destroy(_handle);
    }
    internal Renderer(nint window, int index, bool vsync = false, bool software = false, bool targetTexture = false)
    {
        var flags = RendererFlag.None;
        if(vsync) flags |= RendererFlag.PresentVSync;
        if (software) flags |= RendererFlag.Software;
        else flags |= RendererFlag.Accelerated;
        if (targetTexture) flags |= RendererFlag.TargetTexture;
        _handle = SdlNative.Renderer.Create(window, index, flags);
    }
    public void DrawRectangle(Rectangle<float> rect, Color color, BlendMode blendMode = BlendMode.None, bool filled = true)
    {
        if(_disposed) throw new ObjectDisposedException(nameof(Renderer));
        DrawColor = color;
        DrawBlendMode = blendMode;
        if(filled) SdlNative.Renderer.FillRectangle(_handle, rect);
        else SdlNative.Renderer.DrawRectangle(_handle, rect);
    }
    public void DrawTexture(Texture texture, Color color, BlendMode blend = BlendMode.None, Rectangle<int>? src = null, Rectangle<float>? dest = null, double angle = 0, Point<float>? center = null)
    {
        if(_disposed) throw new ObjectDisposedException(nameof(Renderer));
        texture.Color = color;
        texture.BlendMode = blend;
        SdlNative.Renderer.Copy(_handle, texture.Handle, src, dest, angle, center);
    }
    public void Clear(Color color)
    {
        if(_disposed) throw new ObjectDisposedException(nameof(Renderer));
        DrawColor = color;
        SdlNative.Renderer.Clear(_handle);
    }
    public void Present()
    {
        if(_disposed) throw new ObjectDisposedException(nameof(Renderer));
        SdlNative.Renderer.Present(_handle);
    }
    internal nint Handle
    {
        get
        {
            if(_disposed) throw new ObjectDisposedException(nameof(Renderer));
            return _handle;
        }
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
            SdlNative.Renderer.SetDrawColor(_handle, value);
            _drawColor = value;
        }
    }
    private BlendMode DrawBlendMode
    {
        get => _drawBlendMode;
        set
        {
            if(_drawBlendMode == value) return;
            SdlNative.Renderer.SetDrawBlendMode(_handle, value);
            _drawBlendMode = value;
        }
    }
}