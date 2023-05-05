using System.Diagnostics;
using Directionful.SDL.Video;

namespace Directionful.SDL;

public class SDL : IDisposable
{
    public SDL()
    {
        Native.SDL.Init(Native.Flag.InitFlag.Video);
        Video = new VideoSystem();
    }
    ~SDL() => Dispose();
    public void Dispose()
    {
        if (_disposed) return;
        _disposed = true;
        GC.SuppressFinalize(this);
        Video.Dispose();
        Native.SDL.Quit();
    }
    public VideoSystem Video { get; init; }
    private bool _disposed;
}