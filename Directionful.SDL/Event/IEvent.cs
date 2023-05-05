namespace Directionful.SDL.Event;

public interface IEvent
{
    public EventType Type {get;}
    public uint Timestamp {get;}
}