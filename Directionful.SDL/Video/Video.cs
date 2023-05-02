namespace Directionful.SDL.Video;

public class Video : IDisposable
{
    private bool _disposed;

    public void Dispose()
    {
        if(_disposed) return;
        _disposed = true;
        GC.SuppressFinalize(this);
    }
}