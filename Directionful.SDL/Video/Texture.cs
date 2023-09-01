using Directionful.SDL.Util;

namespace Directionful.SDL.Video;

public class Texture : IDisposable
{
    public void Dispose()
    {
        if(_disposed) return;
        _disposed = true;
        GC.SuppressFinalize(this);
        SdlNative.Texture.Destroy(_handle);
    }
    public Texture(Renderer renderer, Surface surface)
    {
        _handle = SdlNative.Texture.CreateFromSurface(renderer.Handle, surface.Handle);
    }
    internal nint Handle
    {
        get
        {
            if(_disposed) throw new ObjectDisposedException(nameof(Texture));
            return _handle;
        }
    }
    internal Color Color
    {
        get
        {
            if(_disposed) throw new ObjectDisposedException(nameof(Texture));
            return _color;
        }
        set
        {
            if(_disposed) throw new ObjectDisposedException(nameof(Texture));
            if(_color == value) return;
            if(_color.R != value.R || _color.G != value.G || _color.B != value.B ) SdlNative.Texture.SetColorMod(_handle, value.R, value.G, value.B);
            if(_color.A != value.A) SdlNative.Texture.SetColorAlpha(_handle, value.A);
            _color = value;
        }
    }
    internal BlendMode BlendMode
    {
        get
        {
            if(_disposed) throw new ObjectDisposedException(nameof(Texture));
            return _blendMode;
        }
        set
        {
            if(_disposed) throw new ObjectDisposedException(nameof(Texture));
            if(_blendMode == value) return;
            SdlNative.Texture.SetBlendMode(_handle, value);
            _blendMode = value;
        }
    }
    private readonly nint _handle;
    private bool _disposed;
    private Color _color = Color.White;
    private BlendMode _blendMode = BlendMode.None;
}