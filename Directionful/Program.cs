using System.Diagnostics;
using System.Runtime;
using Directionful.SDL;
using Directionful.SDL.Util;
using Directionful.SDL.Video;
GCSettings.LatencyMode = GCLatencyMode.LowLatency;
using var sdl = new SDL(InitFlag.Video);
var window = sdl.Video.CreateWindow("Directionful", new Rectangle<int>(100, 100, 1280, 720), WindowFlag.Resizable);
window.HitTest = (window, area, data) =>
{
    Debug.WriteLine(sdl.Flags);
    return HitTestResult.Draggable;
};
var evt = sdl.Event;
var running = true;
evt.OnQuit = _ => running = false;
var stopwatch = Stopwatch.StartNew();
var lastFpsTimestamp = 0L;
var lastFpsIdx = 0;
while(running)
{
    var current = stopwatch.ElapsedTicks;
    var elapsed = current - lastFpsTimestamp;
    if((current - lastFpsTimestamp) > Stopwatch.Frequency)
    {
        var fpsElapsed = (float) (current - lastFpsTimestamp) / lastFpsIdx;
        var fps = Stopwatch.Frequency/fpsElapsed;
        lastFpsTimestamp = current;
        lastFpsIdx = 0;
        Debug.WriteLine(fps);
        Debug.WriteLine(sdl.Video.Clipboard);
    }
    evt.ProcessEvents();
    window.Title = $"Directionful - {DateTime.Now}";
    lastFpsIdx++;
}