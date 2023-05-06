namespace Directionful.SDL.Event;

public class EventSystem : IDisposable
{
    public void Dispose()
    {
        if(_disposed) return;
        _disposed = true;
        GC.SuppressFinalize(this);
    }
    public void ProcessEvents()
    {
        if(_disposed) throw new ObjectDisposedException(nameof(Event));
        while(Native.SDL.Event.Poll()) {}
    }
    internal EventSystem() {}
    private bool _disposed;
}