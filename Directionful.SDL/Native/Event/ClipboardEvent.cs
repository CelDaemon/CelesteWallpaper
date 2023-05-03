namespace Directionful.SDL.Event;

public readonly partial record struct ClipboardEvent : IEvent
{
    static partial void FromData(ref ClipboardEvent evt, nint data)
    {
        unsafe
        {
            var type = *(EventType*)data;
            var timestamp = *(uint*)data;
            evt = evt with {Type = type, Timestamp = timestamp};
        }
    }
}