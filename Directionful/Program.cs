using System.Diagnostics;
using System.Runtime;
using Directionful.SDL;
using Directionful.SDL.Video.Windowing;
// <3
GCSettings.LatencyMode = GCLatencyMode.LowLatency;
using var sdl = new SDL();
using var window = new Window("Test", new Directionful.SDL.Util.Rectangle<int>(100, 100, 400, 400));
using var video = sdl.Video;
using var evt = sdl.Event;
var stopwatch = Stopwatch.StartNew();
var flashed = false;
while (true)
{
    if (stopwatch.ElapsedMilliseconds > 10000)
    {
        if(!flashed)
        {
            window.Flash(false);
            flashed = true;
        }
    }
    evt.ProcessEvents();
}