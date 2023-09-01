using Directionful.SDL.Enum;
using Directionful.SDL.Event;
using Directionful.SDL.Image;
using Directionful.SDL.Video;

namespace Directionful.SDL;

public class Sdl : IDisposable
{
    public Sdl()
    {
        SdlNative.Init(InitFlag.Video);
        Video = new VideoSystem();
        Event = new EventSystem(Video);
        Image = new ImageSystem();
    }
    ~Sdl() => Dispose();
    public void Dispose()
    {
        if (_disposed) return;
        _disposed = true;
        GC.SuppressFinalize(this);
        Video.Dispose();
        SdlNative.Quit();
    }
    public VideoSystem Video { get; init; }
    public EventSystem Event { get; init; }
    public ImageSystem Image {get; init;}
    private bool _disposed;
}