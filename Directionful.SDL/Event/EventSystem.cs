using Directionful.SDL.Event.Windowing;
using Directionful.SDL.Video;

namespace Directionful.SDL.Event;

public class EventSystem : IDisposable
{
    public delegate void QuitHandler(QuitEvent evt);
    public void Dispose()
    {
        if (_disposed) return;
        _disposed = true;
        GC.SuppressFinalize(this);
    }
    public void ProcessEvents()
    {
        if (_disposed) throw new ObjectDisposedException(nameof(Event));
        while (Native.SDL.Event.Poll(out var evt)) {
            switch(evt)
            {
                case QuitEvent quitEvt:
                    OnQuit?.Invoke(quitEvt);
                    break;
                case WindowEvent windowEvt:
                    _video.GetWindow(windowEvt.WindowID).HandleEvent(windowEvt);
                    break;
            }
        }
    }
    public QuitHandler? OnQuit {get;set;}
    internal EventSystem(VideoSystem video) { _video = video; }
    private readonly VideoSystem _video;
    private bool _disposed;
}