namespace Directionful.SDL.Event;

public readonly partial record struct UnknownEvent(EventType Type, uint Timestamp) : IEvent
{
    public static UnknownEvent FromData(nint data)
    {
        var evt = default(UnknownEvent);
        FromData(ref evt, data);
        return evt;
    }
    static partial void FromData(ref UnknownEvent evt, nint data);
}