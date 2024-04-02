using System.Runtime;
using Directionful;
using Directionful.SDL;
using Directionful.SDL.Util;
using Directionful.SDL.Video;
using Directionful.SDL.Video.Windowing;
// <3
GCSettings.LatencyMode = GCLatencyMode.LowLatency;
using var sdl = new Sdl();
using var video = sdl.Video;
// ReSharper disable once StringLiteralTypo
using var window = new Window(video, "Directionful - Celeste", new Rectangle<int>(320, 180, 1280, 720));
using var renderer = window.Renderer;
using var evt = sdl.Event;
var running = true;
evt.QuitEvent += (_, _) => running = false;
using var snowSurface = sdl.Image.LoadImage("assets/snow.png");
using var snowTexture = new Texture(renderer, snowSurface);
using var overlaySurface = sdl.Image.LoadImage("assets/overlay.png");
using var overlayTexture = new Texture(renderer, overlaySurface);
using var logoSurface = sdl.Image.LoadImage("assets/logo.png");
using var logoTexture = new Texture(renderer, logoSurface);
var snow = new SnowRenderer(window, renderer, snowTexture, overlayTexture);
while (running)
{
    evt.ProcessEvents();
    if(!window.Hidden) snow.Update();
    renderer.Clear(Color.Black);
    var scaleX = window.Location.Width / 1600f;
    var scaleY = window.Location.Height / 900f;
    var minScale = MathF.Min(scaleX, scaleY);


    renderer.DrawTexture(logoTexture, Color.White, dest: Rectangle<float>.Centered(new Vector2<float>((float) window.Location.Width / 2,  (float) window.Location.Height / 2), new Vector2<float>(1728 * minScale, 972 * minScale)));
    snow.Render();
    renderer.Present();
}
