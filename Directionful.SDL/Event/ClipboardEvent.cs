namespace Directionful.SDL.Event;

public readonly partial record struct ClipboardEvent(EventType Type, uint Timestamp) : IEvent
{
    public static ClipboardEvent FromData(nint data)
    {
        var evt = default(ClipboardEvent);
        FromData(ref evt, data);
        return evt;
    }
    static partial void FromData(ref ClipboardEvent evt, nint data);
}