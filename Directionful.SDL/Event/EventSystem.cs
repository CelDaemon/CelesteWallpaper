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
            }
        }
    }
    public QuitHandler? OnQuit {get;set;}
    internal EventSystem() { }
    private bool _disposed;
}