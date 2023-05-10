namespace Directionful.SDL.Event.Windowing;

public readonly partial record struct WindowEvent(uint Timestamp, uint WindowID, WindowEventType Type, int Data1, int Data2) : IEvent;