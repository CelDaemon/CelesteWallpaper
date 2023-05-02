namespace Directionful.SDL.Event;

public class Event : IDisposable
{
    private bool _disposed;
    public void ProcessEvents()
    {
        if(_disposed) throw new ObjectDisposedException(nameof(Event));
        IEvent? evt = null;
        while(Native.SDL.Event.Poll(ref evt))
        {

        }
    }
    public void Dispose()
    {
        if(_disposed) return;
        _disposed = true;
        GC.SuppressFinalize(this);
    }
}