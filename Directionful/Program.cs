using System.Diagnostics;
using Directionful.SDL;
using Directionful.SDL.Video;

using var sdl = new SDL(InitFlag.Video);
var window = new Window("Directionful", new Directionful.SDL.Util.Rectangle<int>(100, 100, 1280, 720), WindowFlag.None);
var evt = sdl.Event;
while(true)
{
    evt.ProcessEvents();
}