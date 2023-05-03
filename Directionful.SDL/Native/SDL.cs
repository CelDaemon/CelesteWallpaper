using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Text;
using Directionful.SDL.Event;
using Directionful.SDL.Util;
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
                EventType.Window => WindowEvent.FromData((nint)uEvt),
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
                Encoding.UTF8.GetBytes(titleFixed, title.Length, uTitle, uTitleSize - 1);
            }
            *(uTitle + uTitleSize - 1) = 0;
            var ret = _CreateWindow((nint) uTitle, x, y, w, h, (uint) flags);
            if(ret == nint.Zero) throw new SDLException("Failed to create window");
            return ret;
            [DllImport("SDL2", EntryPoint = "SDL_CreateWindow")]
            static extern nint _CreateWindow(nint title, int x, int y, int w, int h, uint flags);
        }
        public static void SetTitle(nint window, string title)
        {
            var uTitleSize = Encoding.UTF8.GetMaxByteCount(title.Length) + 1;
            byte* uTitle = stackalloc byte[uTitleSize];
            fixed (char* titleFixed = title)
            {
                Encoding.UTF8.GetBytes(titleFixed, title.Length, uTitle, uTitleSize - 1);
            }
            *(uTitle + uTitleSize - 1) = 0;
            _SetWindowTitle(window, (nint) uTitle);
            [DllImport("SDL2", EntryPoint = "SDL_SetWindowTitle")]
            static extern void _SetWindowTitle(nint window, nint title);
        }
        public static void SetPosition(nint window, int x, int y)
        {
            _SetWindowPosition(window, x, y);
            [DllImport("SDL2", EntryPoint = "SDL_SetWindowPosition")]
            static extern void _SetWindowPosition(nint window, int x, int y);
        }
        public static void SetSize(nint window, int width, int height)
        {
            _SetWindowSize(window, width, height);
            [DllImport("SDL2", EntryPoint = "SDL_SetWindowSize")]
            static extern void _SetWindowSize(nint window, int width, int height);
        }
        public static uint GetID(nint window)
        {
            var ret = _GetWindowID(window);
            if(ret == 0) throw new SDLException("Failed to get window ID");
            return ret;
            [DllImport("SDL2", EntryPoint = "SDL_GetWindowID")]
            static extern uint _GetWindowID(nint window);
        }
        public static void Destroy(nint window)
        {
            _DestroyWindow(window);
            [DllImport("SDL2", EntryPoint = "SDL_DestroyWindow")]
            static extern void _DestroyWindow(nint window);
        }
        
        public static void SetHitTest(nint window, uint windowID)
        {
            [UnmanagedCallersOnly]
            static int HitTest(nint window, nint area, nint data)
            {
                var windowID = (uint) data;
                var point = Point<int>.FromData(area);
                var mWindow = Directionful.SDL.SDL.Instance.Video.GetWindow(windowID);
                return (int) mWindow.HitTest!.Invoke(mWindow, point);
            }
            delegate*unmanaged<nint, nint, nint, int> callback = &HitTest;
            if(_SetWindowHitTest(window, (nint)callback, (nint) windowID) != 0) throw new SDLException("Failed to set hit test");
            [DllImport("SDL2", EntryPoint = "SDL_SetWindowHitTest")]
            static extern int _SetWindowHitTest(nint window, nint callback, nint data);
        }
        public static void RemoveHitTest(nint window)
        {
            if(_SetWindowHitTest(window, nint.Zero, nint.Zero) != 0) throw new SDLException("Failed to remove hit test");
            [DllImport("SDL2", EntryPoint = "SDL_SetWindowHitTest")]
            static extern int _SetWindowHitTest(nint window, nint callback, nint data);
        }
    }
    public static string? GetError()
    {
        return Marshal.PtrToStringAnsi(_GetError());
        [DllImport("SDL2", EntryPoint = "SDL_GetError")]
        static extern nint _GetError();
    }
}