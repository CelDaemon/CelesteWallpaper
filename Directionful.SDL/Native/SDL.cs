using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Text;
using Directionful.SDL.Event;
using Directionful.SDL.Util;
using Directionful.SDL.Video;

namespace Directionful.SDL.Native;

public static unsafe class SDL
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
                EventType.ClipboardUpdate => ClipboardUpdateEvent.FromData((nint)uEvt),
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
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int HitTestHandler(nint window, nint area, nint data);
        [SupportedOSPlatform("windows")]
        public static void SetHitTest(nint window, HitTestHandler callback, nint data)
        {
            if(_SetWindowHitTest(window, Marshal.GetFunctionPointerForDelegate(callback), data) != 0) throw new SDLException("Failed to set hit test");
            [DllImport("SDL2", EntryPoint = "SDL_SetWindowHitTest")]
            static extern int _SetWindowHitTest(nint window, nint callback, nint data);
        }
        public static void RemoveHitTest(nint window)
        {
            if(_SetWindowHitTest(window, nint.Zero, nint.Zero) != 0) throw new SDLException("Failed to remove hit test");
            [DllImport("SDL2", EntryPoint = "SDL_SetWindowHitTest")]
            static extern int _SetWindowHitTest(nint window, nint callback, nint data);
        }
        public static void SetMinimumSize(nint window, Size<int> size)
        {
            _SetWindowMimimumSize(window, size.Width, size.Height);
            [DllImport("SDL2", EntryPoint = "SDL_SetWindowMinimumSize")]
            static extern void _SetWindowMimimumSize(nint window, int w, int h);
        }
        public static void SetMaximumSize(nint window, Size<int> size)
        {
            _SetWindowMaximumSize(window, size.Width, size.Height);
            [DllImport("SDL2", EntryPoint = "SDL_SetWindowMaxiumumSize")]
            static extern void _SetWindowMaximumSize(nint window, int w, int h);
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
            if(_SetWindowFullscreen(window, flags) != 0) throw new SDLException("Failed to set fullscreen");
            [DllImport("SDL2", EntryPoint = "SDL_SetWindowFullscreen")]
            static extern int _SetWindowFullscreen(nint window, WindowFlag flags);
        }
    }
    public static class ScreenSaver
    {
        public static void Enable()
        {
            _EnableScreenSaver();
            [DllImport("SDL2", EntryPoint = "SDL_EnableScreenSaver")]
            static extern void _EnableScreenSaver();
        }
        public static void Disable()
        {
            _DisableScreenSaver();
            [DllImport("SDL2", EntryPoint = "SDL_DisableScreenSaver")]
            static extern void _DisableScreenSaver();
        }
    }
    public static class Clipboard
    {
        public static void SetText(string text)
        {
            var uTextSize = Encoding.UTF8.GetMaxByteCount(text.Length) + 1;
            byte* uText = stackalloc byte[uTextSize];
            fixed(char* textFixed = text)
            {
                Encoding.UTF8.GetBytes(textFixed, text.Length, uText, uTextSize - 1);
            }
            *(uText + uTextSize - 1) = 0;
            if(_SetClipboardText((nint) uText) != 0) throw new SDLException("Failed to set clipboard");
            [DllImport("SDL2", EntryPoint = "SDL_SetClipboardText")]
            static extern int _SetClipboardText(nint text);
        }
        public static string? GetText()
        {
            var uText = _GetClipboardText();
            if(uText == nint.Zero) throw new SDLException("Failed to get clipboard");
            var text = Marshal.PtrToStringUTF8(uText);
            if(string.IsNullOrEmpty(text)) return null;
            return text;
            [DllImport("SDL2", EntryPoint = "SDL_GetClipboardText")]
            static extern nint _GetClipboardText();
        }
        public static void ClearText()
        {
            if(_SetClipboardText(0) != 0) throw new SDLException("Failed to set clipboard");
            [DllImport("SDL2", EntryPoint = "SDL_SetClipboardText")]
            static extern int _SetClipboardText(nint text);
        }
    }
    public static string? GetError()
    {
        return Marshal.PtrToStringAnsi(_GetError());
        [DllImport("SDL2", EntryPoint = "SDL_GetError")]
        static extern nint _GetError();
    }
}