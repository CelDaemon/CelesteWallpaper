namespace Directionful.SDL.Event;

public readonly unsafe partial record struct UnknownEvent
{
    internal static UnknownEvent FromData(byte* data)
    {
        return new UnknownEvent(*(uint*)(data+4));
    }
}