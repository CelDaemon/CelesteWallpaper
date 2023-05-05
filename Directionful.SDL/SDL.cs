using System.Diagnostics;

namespace Directionful.SDL;

public class SDL : IDisposable
{
    public bool _disposed;
    public SDL()
    {
        Native.SDL.Init(Native.Flag.InitFlag.Video);
    }
    ~SDL() => Dispose();
    public void Dispose()
    {
        if(_disposed) return;
        _disposed = true;
        GC.SuppressFinalize(this);
        Native.SDL.Quit();
        Debug.WriteLine("Quit");
    }
}