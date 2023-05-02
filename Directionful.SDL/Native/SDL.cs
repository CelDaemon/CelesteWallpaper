using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Directionful.SDL.Event;
using Directionful.SDL.Video;

namespace Directionful.SDL.Native;

internal static unsafe class SDL
{
    public static void Init(InitFlag flags)
    {
        if(_Init((uint) flags) != 0) throw new SDLException("Failed to initialize SDL");
        [DllImport("SDL2", EntryPoint = "SDL_Init")]
        static extern int _Init(uint flags);
    }
    public static void Quit()
    {
        _Quit();
        [DllImport("SDL2", EntryPoint = "SDL_Quit")]
        static extern void _Quit();
    }
    public static class Event
    {
        public static bool Poll([NotNullWhen(true)] ref IEvent? evt)
        {
            var uEvt = stackalloc byte[56];
            if(_PollEvent((nint)uEvt) != 1)
            {
                evt = null;
                return false;
            }
            var type = *(EventType*)uEvt;
            evt = type switch
            {
                EventType.Quit => QuitEvent.FromData((nint)uEvt),
                _ => UnknownEvent.FromData((nint)uEvt)
            };
            return true;
            [DllImport("SDL2", EntryPoint = "SDL_PollEvent")]
            static extern int _PollEvent(nint evt);
        }
    }
    public static class Window
    {
        public static nint Create(string title, int x, int y, int w, int h, WindowFlag flags)
        {
            var uTitleSize = Encoding.UTF8.GetMaxByteCount(title.Length) + 1;
            byte* uTitle = stackalloc byte[uTitleSize];
            fixed (char* titleFixed = title)
            {
                Encoding.UTF8.GetBytes(titleFixed, title.Length, uTitle, uTitleSize);
            }
            *(uTitle + uTitleSize - 1) = 0;
            var ret = _CreateWindow((nint) uTitle, x, y, w, h, (uint) flags);
            if(ret == nint.Zero) throw new SDLException("Failed to create window");
            return ret;
            [DllImport("SDL2", EntryPoint = "SDL_CreateWindow")]
            static extern nint _CreateWindow(nint title, int x, int y, int w, int h, uint flags);
        }
        public static void Destroy(nint window)
        {
            _DestroyWindow(window);
            [DllImport("SDL2", EntryPoint = "SDL_DestroyWindow")]
            static extern void _DestroyWindow(nint window);
        }
    }
    public static string? GetError()
    {
        return Marshal.PtrToStringAnsi(_GetError());
        [DllImport("SDL2", EntryPoint = "SDL_GetError")]
        static extern nint _GetError();
    }
}