namespace Directionful.SDL.Event;

public readonly partial record struct UnknownEvent(EventType Type, uint Timestamp) : IEvent
{
    
}