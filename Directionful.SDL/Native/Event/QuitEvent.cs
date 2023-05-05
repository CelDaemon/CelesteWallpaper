namespace Directionful.SDL.Event;

public unsafe readonly partial record struct QuitEvent : IEvent
{
    public static QuitEvent FromData(nint data)
    {
        var uData = (byte*)data;
        var type = *(EventType*)uData;
        var timestamp = *(uint*)(uData+4);
        return new QuitEvent(type, timestamp);
    }
}