namespace Directionful.SDL.Event;

public unsafe readonly partial record struct QuitEvent : IEvent
{
    internal static QuitEvent FromData(byte* data)
    {
        return new QuitEvent(*(uint*)(data+4));
    }
}