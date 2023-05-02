namespace Directionful.SDL.Event;

public readonly partial record struct WindowEvent : IEvent
{
    static partial void FromData(ref WindowEvent evt, nint data)
    {
        unsafe
        {
            var uData = (byte*)data;
            var type = *(EventType*)uData;
            var timestamp = *(uint*)(uData+4);
            var windowID = *(uint*)(uData+8);
            var windowEventType = *(WindowEventType*)(uData+12);
            var data1 = *(int*)(uData+16);
            var data2 = *(int*)(uData+20);
            evt = new WindowEvent(type, timestamp, windowID, windowEventType, data1, data2);
        }
    }
}