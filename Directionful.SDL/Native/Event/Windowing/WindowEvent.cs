namespace Directionful.SDL.Event.Windowing;

public unsafe readonly partial record struct WindowEvent : IEvent
{
    internal static WindowEvent FromData(byte* data)
    {
        return new WindowEvent(
            *(uint*)(data+4),
            *(uint*)(data+8),
            *(WindowEventType*)(data+12),
            *(int*)(data+16),
            *(int*)(data+20)
        );
    }
}