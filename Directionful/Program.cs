using System.Diagnostics;
using System.Runtime;
using Directionful.SDL;
using Directionful.SDL.Util;
using Directionful.SDL.Video.Windowing;
// <3
GCSettings.LatencyMode = GCLatencyMode.LowLatency;
using var sdl = new SDL();
using var video = sdl.Video;
using var window = new Window(video, "Test", new Rectangle<int>(320, 180, 1280, 720));
using var evt = sdl.Event;
var stopwatch = Stopwatch.StartNew();
var flashed = false;
var running = true;
evt.OnQuit += _ => running = false;
while (running)
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