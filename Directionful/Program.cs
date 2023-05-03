using System.Diagnostics;
using Directionful.SDL;
using Directionful.SDL.Util;
using Directionful.SDL.Video;

using var sdl = new SDL(InitFlag.Video);
var window = sdl.Video.CreateWindow("Directionful", new Rectangle<int>(100, 100, 1280, 720), WindowFlag.Resizable);
window.HitTest = (window, point) =>
{
    Debug.WriteLine($"{window}, {point}");
    return HitTestResult.ResizeRight;
};
var evt = sdl.Event;
var running = true;
evt.OnQuit = _ => running = false;
while(running)
{
    evt.ProcessEvents();
    window.Title = $"Directionful - {DateTime.Now}";
}