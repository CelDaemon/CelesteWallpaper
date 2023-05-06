using System.Diagnostics;
using Directionful.SDL.Event;
using Directionful.SDL.Video;

namespace Directionful.SDL;

public class SDL : IDisposable
{
    public SDL()
    {
        Native.SDL.Init(Native.Flag.InitFlag.Video);
        Video = new VideoSystem();
        Event = new EventSystem();
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
    public EventSystem Event { get; init; }
    private bool _disposed;
}