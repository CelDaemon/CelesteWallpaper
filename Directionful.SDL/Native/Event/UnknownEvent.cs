namespace Directionful.SDL.Event;

public unsafe readonly partial record struct UnknownEvent : IEvent
{
    internal static UnknownEvent FromData(byte* data)
    {
        return new UnknownEvent(*(uint*)(data+4));
    }
}