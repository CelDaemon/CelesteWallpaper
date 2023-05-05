namespace Directionful.SDL.Event;

public readonly partial record struct WindowEvent(EventType Type, uint Timestamp, uint WindowID, WindowEventType Event, int Data1, int Data2) : IEvent
{
    
}