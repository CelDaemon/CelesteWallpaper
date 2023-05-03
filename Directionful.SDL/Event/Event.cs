namespace Directionful.SDL.Event;

public class Event : IDisposable
{
    public delegate void QuitHandler(QuitEvent evt);
    public QuitHandler? OnQuit;
    private readonly Video.Video? _video;
    private bool _disposed;
    internal Event(Video.Video? video)
    {
        _video = video;
    }
    public void ProcessEvents()
    {
        if(_disposed) throw new ObjectDisposedException(nameof(Event));
        IEvent? evt = null;
        while(Native.SDL.Event.Poll(ref evt))
        {
            switch(evt.Type)
            {
                case EventType.Quit:
                    OnQuit?.Invoke((QuitEvent) evt);
                    break;
                case EventType.Window:
                    var nEvt = (WindowEvent) evt;
                    _video?.GetWindow(nEvt.WindowID).HandleEvent(nEvt);
                    break;
                case EventType.ClipboardUpdate:
                    _video?.HandleEvent((ClipboardEvent) evt);
                    break;
            }
        }
    }
    public void Dispose()
    {
        if(_disposed) return;
        _disposed = true;
        GC.SuppressFinalize(this);
    }
}