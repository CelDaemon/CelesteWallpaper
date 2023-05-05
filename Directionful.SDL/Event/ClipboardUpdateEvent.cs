namespace Directionful.SDL.Event;

public readonly partial record struct ClipboardUpdateEvent(EventType Type, uint Timestamp) : IEvent
{
    
}