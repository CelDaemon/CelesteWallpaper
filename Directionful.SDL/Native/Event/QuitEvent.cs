namespace Directionful.SDL.Event;

public readonly unsafe partial record struct QuitEvent
{
    internal static QuitEvent FromData(byte* data)
    {
        return new QuitEvent(*(uint*)(data+4));
    }
}