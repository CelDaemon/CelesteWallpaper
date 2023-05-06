using System.Runtime;
using Directionful.SDL;
using Directionful.SDL.Video.Windowing;

GCSettings.LatencyMode = GCLatencyMode.LowLatency;
using var sdl = new SDL();
using var window = new Window("Test", new Directionful.SDL.Util.Rectangle<int>(100, 100, 400, 400), false);
using var video = sdl.Video;
while(true) {}