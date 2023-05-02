using System.Diagnostics;

namespace Directionful.SDL.Event;

public class Event : IDisposable
{
    public delegate void QuitHandler(QuitEvent evt);
    private bool _disposed;
    public QuitHandler? OnQuit;
    public void ProcessEvents()
    {
        if(_disposed) throw new ObjectDisposedException(nameof(Event));
        IEvent? evt = null;
        var stopwatch = Stopwatch.StartNew();
        while(Native.SDL.Event.Poll(ref evt))
        {
            Debug.WriteLine($"Poll event delay: {stopwatch.ElapsedTicks}");
            switch(evt.Type)
            {
                case EventType.Quit:
                    OnQuit?.Invoke((QuitEvent) evt);
                    break;
            }
            stopwatch.Restart();
        }
    }
    public void Dispose()
    {
        if(_disposed) return;
        _disposed = true;
        GC.SuppressFinalize(this);
    }
}