namespace Directionful.SDL.Event;

public readonly partial record struct UnknownEvent : IEvent
{
    static partial void FromData(ref UnknownEvent evt, nint data)
    {
        unsafe
        {
            var uData = (byte*)data;
            var type = *(EventType*)uData;
            var timestamp = *(uint*)(uData+4);
            evt = evt with {Type = type, Timestamp = timestamp};
        }
    }
}