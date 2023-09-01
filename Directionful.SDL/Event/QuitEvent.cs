namespace Directionful.SDL.Event;

public readonly partial record struct QuitEvent(uint Timestamp) : IEvent;