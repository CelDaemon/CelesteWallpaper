using System.Runtime.InteropServices;
using System.Text;
using Directionful.SDL.Native.Flag;
using Directionful.SDL.Util;
using Directionful.SDL.Video.Windowing;

namespace Directionful.SDL.Native;

internal static unsafe class SDL
{
    public static void Init(InitFlag flags)
    {
        if (_Init((uint)flags) != 0) throw new SDLException("Failed to initialize SDL");
        [DllImport("SDL2", EntryPoint = "SDL_Init")]
        static extern int _Init(uint flags);
    }
    public static void Quit()
    {
        _Quit();
        [DllImport("SDL2", EntryPoint = "SDL_Quit")]
        static extern void _Quit();
    }
    public static class Error
    {
        public static string? Get()
        {
            var retPtr = _GetError();
            if (retPtr == nint.Zero) return null;
            var ret = new string((sbyte*)retPtr);
            if (string.IsNullOrEmpty(ret)) return null;
            return ret;
            [DllImport("SDL2", EntryPoint = "SDL_GetError")]
            static extern nint _GetError();
        }
    }
    public static class Window
    {
        public static nint Create(string title, Rectangle<int> location, WindowFlag flags)
        {
            var uTitleLength = Encoding.UTF8.GetMaxByteCount(title.Length) + 1;
            var uTitle = stackalloc byte[uTitleLength];
            fixed (char* titlePtr = title)
            {
                Encoding.UTF8.GetBytes(titlePtr, title.Length, uTitle, uTitleLength - 1);
            }
            *(uTitle + uTitleLength - 1) = 0;
            var ret = _CreateWindow((nint)uTitle, location.X, location.Y, location.Width, location.Height, (uint)flags);
            if (ret == nint.Zero) throw new SDLException("Failed to create window");
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
        public static void SetResizable(nint window, bool resizable)
        {
            _SetWindowResizable(window, resizable ? 1 : 0);
            [DllImport("SDL2", EntryPoint = "SDL_SetWindowResizable")]
            static extern void _SetWindowResizable(nint window, int resizable);
        }
        public static void SetBordered(nint window, bool bordered)
        {
            _SetWindowBordered(window, bordered ? 1 : 0);
            [DllImport("SDL2", EntryPoint = "SDL_SetWindowBordered")]
            static extern void _SetWindowBordered(nint window, int bordered);
        }
        public static void SetAlwaysOnTop(nint window, bool alwaysOnTop)
        {
            _SetWindowAlwaysOnTop(window, alwaysOnTop ? 1 : 0);
            [DllImport("SDL2", EntryPoint = "SDL_SetWindowAlwaysOnTop")]
            static extern void _SetWindowAlwaysOnTop(nint window, int alwaysOnTop);
        }
        public static void Show(nint window)
        {
            _ShowWindow(window);
            [DllImport("SDL2", EntryPoint = "SDL_ShowWindow")]
            static extern void _ShowWindow(nint window);
        }
        public static void Hide(nint window)
        {
            _HideWindow(window);
            [DllImport("SDL2", EntryPoint = "SDL_HideWindow")]
            static extern void _HideWindow(nint window);
        }
        public static void Maximize(nint window)
        {
            _MaximizeWindow(window);
            [DllImport("SDL2", EntryPoint = "SDL_MaximizeWindow")]
            static extern void _MaximizeWindow(nint window);
        }
        public static void Minimize(nint window)
        {
            _MinimizeWindow(window);
            [DllImport("SDL2", EntryPoint = "SDL_MinimizeWindow")]
            static extern void _MinimizeWindow(nint window);
        }
        public static void Restore(nint window)
        {
            _RestoreWindow(window);
            [DllImport("SDL2", EntryPoint = "SDL_RestoreWindow")]
            static extern void _RestoreWindow(nint window);
        }
        public static void SetFullscreen(nint window, WindowFlag flags)
        {
            if (_SetWindowFullscreen(window, (uint)flags) != 0) throw new SDLException("Failed to set fullscreen");
            [DllImport("SDL2", EntryPoint = "SDL_SetWindowFullscreen")]
            static extern int _SetWindowFullscreen(nint window, uint flags);
        }
        public static void SetOpacity(nint window, float opacity)
        {
            if(_SetWindowOpacity(window, opacity) != 0) throw new SDLException("Failed to set opacity");
            [DllImport("SDL2", EntryPoint = "SDL_SetWindowOpacity")]
            static extern int _SetWindowOpacity(nint window, float opacity);
        }
    }
    public static class Event
    {
        public static bool Poll()
        {
            var evt = stackalloc byte[56];
            return _PollEvent((nint)evt) == 1;
            [DllImport("SDL2", EntryPoint = "SDL_PollEvent")]
            static extern int _PollEvent(nint evt);
        }
    }
}