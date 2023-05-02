namespace Directionful.SDL.Event;

public readonly partial record struct QuitEvent : IEvent
{
    static partial void FromData(ref QuitEvent evt, nint data)
    {
        unsafe
        {
            var uData = (byte*)data;
            var type = *(EventType*)uData;
            var timestamp = *(uint*)(uData+4);
            evt = new QuitEvent(type, timestamp);
        }
    }
}