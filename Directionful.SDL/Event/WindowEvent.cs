namespace Directionful.SDL.Event;

public readonly partial record struct WindowEvent(EventType Type, uint Timestamp, uint WindowID, WindowEventType Event, int Data1, int Data2) : IEvent
{
    public static WindowEvent FromData(nint data)
    {
        var evt = default(WindowEvent);
        FromData(ref evt, data);
        return evt;
    }
    static partial void FromData(ref WindowEvent evt, nint data);
}