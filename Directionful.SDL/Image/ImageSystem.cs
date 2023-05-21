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
        return new Surface(Native.SDLImage.Load(path));
    }
    internal ImageSystem() {
        Native.SDLImage.Init();
    }
    private bool _disposed;
}