using System.Diagnostics;
using System.Runtime;
using Directionful;
using Directionful.SDL;
using Directionful.SDL.Util;
using Directionful.SDL.Video;
using Directionful.SDL.Video.Windowing;
// <3
GCSettings.LatencyMode = GCLatencyMode.LowLatency;
using var sdl = new SDL();
using var video = sdl.Video;
using var window = new Window(video, "Directionful - I love you so muchhh Kay <3", new Rectangle<int>(320, 180, 1280, 720),
#if !DEBUG
 hidden: true
#else
 hidden: false
#endif
 );
using var renderer = window.Renderer;
using var evt = sdl.Event;
var stopwatch = Stopwatch.StartNew();
var running = true;
evt.QuitEvent += (_, _) => running = false;
using var snowSurface = sdl.Image.LoadImage("assets/snow.png");
using var snowTexture = new Texture(renderer, snowSurface);
using var overlaySurface = sdl.Image.LoadImage("assets/overlay.png");
using var overlayTexture = new Texture(renderer, overlaySurface);
using var logoSurface = sdl.Image.LoadImage("assets/logo.png");
using var logoTexture = new Texture(renderer, logoSurface);
var snow = new SnowRenderer(window, renderer, snowTexture, overlayTexture);
var deltaTime = 1f;
#if !DEBUG
window.LocationChangedEvent += (_, _) =>
{
    snow.Reset();
    window.Hidden = false;
};
#endif
while (running)
{
    var deltaTimeStamp = stopwatch.ElapsedMilliseconds;
    evt.ProcessEvents();
    if(!window.Hidden) snow.Update(deltaTime);
    renderer.Clear(Color.Black);
    var scalex = window.Location.Width / 1600f;
    var scaley = window.Location.Height / 900f;
    var minscale = MathF.Min(scalex, scaley);


    renderer.DrawTexture(logoTexture, Color.White, BlendMode.None, dest: Rectangle<float>.Centered(new Vector2<float>(window.Location.Width / 2, window.Location.Height / 2), new Vector2<float>(1728 * minscale, 972 * minscale)));
    snow.Render();
    renderer.Present();
    deltaTime = ((float) stopwatch.ElapsedMilliseconds - deltaTimeStamp) / 1000;
}