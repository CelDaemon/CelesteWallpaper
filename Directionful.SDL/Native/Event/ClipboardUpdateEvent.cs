namespace Directionful.SDL.Event;

public unsafe readonly partial record struct ClipboardUpdateEvent : IEvent
{
    public static ClipboardUpdateEvent FromData(nint data)
    {
        var type = *(EventType*)data;
        var timestamp = *(uint*)data;
        return new ClipboardUpdateEvent(type, timestamp);
    }
}