namespace Directionful.SDL.Video;

public class Surface : IDisposable
{
    public void Dispose()
    {
        if (_disposed) return;
        _disposed = true;
        GC.SuppressFinalize(this);
        Native.SDL.Surface.Free(_handle);
    }
    internal Surface(nint handle)
    {
        _handle = handle;
    }
    internal nint Handle
    {
        get
        {
            if(_disposed) throw new ObjectDisposedException(nameof(Surface));
            return _handle;
        }
    }
    private readonly nint _handle;

    private bool _disposed;
}