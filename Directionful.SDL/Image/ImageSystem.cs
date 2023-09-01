using Directionful.SDL.Video;

namespace Directionful.SDL.Image;

public class ImageSystem : IDisposable
{
    public void Dispose()
    {
        if (_disposed) return;
        _disposed = true;
        GC.SuppressFinalize(this);
    }
    public Surface LoadImage(string path)
    {
        if(_disposed) throw new ObjectDisposedException(nameof(ImageSystem));
        return new Surface(SdlImageNative.Load(path));
    }
    internal ImageSystem() {
        SdlImageNative.Init();
    }
    private bool _disposed;
}