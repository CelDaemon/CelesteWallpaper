namespace Directionful.SDL.Event;

public readonly partial record struct QuitEvent(EventType Type, uint Timestamp) : IEvent
{
    
}