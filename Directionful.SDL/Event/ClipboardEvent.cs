namespace Directionful.SDL.Event;

public readonly partial record struct ClipboardEvent(EventType Type, uint Timestamp) : IEvent
{
    
}