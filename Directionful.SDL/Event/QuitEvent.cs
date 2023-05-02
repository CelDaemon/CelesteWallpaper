namespace Directionful.SDL.Event;

public readonly partial record struct QuitEvent(EventType Type, uint Timestamp) : IEvent
{
    public static QuitEvent FromData(nint data)
    {
        var evt = default(QuitEvent);
        FromData(ref evt, data);
        return evt;
    }
    static partial void FromData(ref QuitEvent evt, nint data);
}