namespace Directionful.SDL.Event;

public readonly partial record struct UnknownEvent(uint Timestamp) : IEvent;