namespace Directionful.SDL.Event;

public unsafe readonly partial record struct UnknownEvent : IEvent
{
    public static UnknownEvent FromData(nint data)
    {
        var uData = (byte*)data;
        var type = *(EventType*)uData;
        var timestamp = *(uint*)(uData+4);
        return new UnknownEvent(type, timestamp);
    }
}