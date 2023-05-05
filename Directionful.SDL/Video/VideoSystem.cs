namespace Directionful.SDL.Video;

public class VideoSystem : IDisposable
{
    public void Dispose()
    {
        if(_disposed) return;
        _disposed = true;
        GC.SuppressFinalize(this);
    }
    internal VideoSystem() {}
    private bool _disposed;
}