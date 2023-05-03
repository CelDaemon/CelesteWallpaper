namespace Directionful.SDL.Event;

public unsafe readonly partial record struct ClipboardEvent : IEvent
{
    public static ClipboardEvent FromData(nint data)
    {
        var type = *(EventType*)data;
        var timestamp = *(uint*)data;
        return new ClipboardEvent(type, timestamp);
    }
}