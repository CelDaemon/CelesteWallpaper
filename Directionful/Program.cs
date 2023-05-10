using System.Diagnostics;
using System.Runtime;
using Directionful.SDL;
using Directionful.SDL.Util;
using Directionful.SDL.Video;
using Directionful.SDL.Video.Windowing;
// <3
GCSettings.LatencyMode = GCLatencyMode.LowLatency;
using var sdl = new SDL();
using var video = sdl.Video;
using var window = new Window(video, "Directionful - I love you so muchhh Kay <3", new Rectangle<int>(320, 180, 1280, 720), hidden: true);
using var renderer = window.Renderer;
using var evt = sdl.Event;
var stopwatch = Stopwatch.StartNew();
var running = true;
evt.OnQuit += _ => running = false;
while (running)
{
    evt.ProcessEvents();
    window.Hidden = false;
    renderer.Clear(Color.Black);
    var t = (float) MathF.Cos((float) stopwatch.Elapsed.TotalSeconds);
    var rectMiddleWidth = window.Location.Width / 2 - 400 / 2;
    var rectMiddleHeight = window.Location.Height / 2 - 400 / 2;
    var rectOffset = 100 * t;
    renderer.DrawRectangle(new Rectangle<float>(rectMiddleWidth - rectOffset, rectMiddleHeight - rectOffset, 400, 400), Color.Purple);
    renderer.DrawRectangle(new Rectangle<float>(rectMiddleWidth + rectOffset, rectMiddleHeight + rectOffset, 400, 400), Color.Blue with {A = 100}, BlendMode.Blend);
    renderer.Present();
}