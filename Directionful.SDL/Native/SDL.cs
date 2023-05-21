using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Text;
using Directionful.SDL.Event;
using Directionful.SDL.Event.Windowing;
using Directionful.SDL.Native.Enum;
using Directionful.SDL.Util;
using Directionful.SDL.Video;
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
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int HitTestHandler(nint window, nint area, nint data);
        [SupportedOSPlatform("Windows")]
        public static void SetHitTest(nint window, HitTestHandler? hitTest, nint data)
        {
            if(_SetWindowHitTest(window, hitTest != null ? Marshal.GetFunctionPointerForDelegate(hitTest) : 0, data) != 0) throw new SDLException("Failed to set hit test");
            [DllImport("SDL2", EntryPoint = "SDL_SetWindowHitTest")]
            static extern int _SetWindowHitTest(nint window, nint hitTest, nint data);
        }
        public static void Flash(nint window, FlashOperation operation)
        {
            if(_FlashWindow(window, (int) operation) != 0) throw new SDLException("Failed to flash window");
            [DllImport("SDL2", EntryPoint = "SDL_FlashWindow")]
            static extern int _FlashWindow(nint window, int operation);
        }
        public static uint GetID(nint window)
        {
            var ret = _GetWindowID(window);
            if(ret == nint.Zero) throw new SDLException("Failed to get window id");
            return ret;
            [DllImport("SDL2", EntryPoint = "SDL_GetWindowID")]
            static extern uint _GetWindowID(nint window);
        }
        public static void SetPosition(nint window, int x, int y)
        {
            _SetWindowPosition(window, x, y);
            [DllImport("SDL2", EntryPoint = "SDL_SetWindowPosition")]
            static extern void _SetWindowPosition(nint window, int x, int y);
        }
        public static void SetSize(nint window, int w, int h)
        {
            _SetWindowSize(window, w, h);
            [DllImport("SDL2", EntryPoint = "SDL_SetWindowSize")]
            static extern void _SetWindowSize(nint window, int w, int h);
        }
        public static void SetTitle(nint window, string title)
        {
            var uTitleLength = Encoding.UTF8.GetMaxByteCount(title.Length) + 1;
            var uTitle = stackalloc byte[uTitleLength];
            fixed (char* titlePtr = title)
            {
                Encoding.UTF8.GetBytes(titlePtr, title.Length, uTitle, uTitleLength - 1);
            }
            *(uTitle + uTitleLength - 1) = 0;
            _SetWindowTitle(window, (nint) uTitle);
            [DllImport("SDL2", EntryPoint = "SDL_SetWindowTitle")]
            static extern void _SetWindowTitle(nint window, nint title);
        }
    }
    public static class Renderer
    {
        public static nint Create(nint window, int index, RendererFlag flags)
        {
            var ret = _CreateRenderer(window, index, (uint) flags);
            if(ret == nint.Zero) throw new SDLException("Failed to create renderer");
            return ret;
            [DllImport("SDL2", EntryPoint = "SDL_CreateRenderer")]
            static extern nint _CreateRenderer(nint window, int index, uint flags);
        }
        public static void Destroy(nint renderer)
        {
            _DestroyRenderer(renderer);
            [DllImport("SDL2", EntryPoint = "SDL_DestroyRenderer")]
            static extern void _DestroyRenderer(nint renderer);
        }
        public static void Present(nint renderer)
        {
            _RenderPresent(renderer);
            [DllImport("SDL2", EntryPoint = "SDL_RenderPresent")]
            static extern void _RenderPresent(nint renderer);
        }
        public static void SetDrawColor(nint renderer, Color color)
        {
            if(_SetRenderDrawColor(renderer, color.R, color.G, color.B, color.A) != 0) throw new SDLException("Failed to set draw color");
            [DllImport("SDL2", EntryPoint = "SDL_SetRenderDrawColor")]
            static extern int _SetRenderDrawColor(nint renderer, byte r, byte g, byte b, byte a);
        }
        public static void Clear(nint renderer)
        {
            if(_RenderClear(renderer) != 0) throw new SDLException("Failed to clear renderer");
            [DllImport("SDL2", EntryPoint = "SDL_RenderClear")]
            static extern int _RenderClear(nint renderer);
        }
        public static void FillRectangle(nint renderer, Rectangle<float> rect)
        {
            var uRect = stackalloc float[4];
            rect.ToData(uRect);
            if(_RenderFillRectF(renderer, (nint) uRect) != 0) throw new SDLException("Failed to fill rectangle");
            [DllImport("SDL2", EntryPoint = "SDL_RenderFillRectF")]
            static extern int _RenderFillRectF(nint renderer, nint rect);
        }
        public static void DrawRectangle(nint renderer, Rectangle<float> rect)
        {
            var uRect = stackalloc float[4];
            rect.ToData(uRect);
            if(_RenderDrawRectF(renderer, (nint) uRect) != 0) throw new SDLException("Failed to fill rectangle");
            [DllImport("SDL2", EntryPoint = "SDL_RenderDrawRectF")]
            static extern int _RenderDrawRectF(nint renderer, nint rect);
        }
        public static void SetDrawBlendMode(nint renderer, BlendMode mode)
        {
            if(_SetRenderDrawBlendMode(renderer, (int) mode) != 0) throw new SDLException("Failed to set draw blend mode");
            [DllImport("SDL2", EntryPoint = "SDL_SetRenderDrawBlendMode")]
            static extern int _SetRenderDrawBlendMode(nint renderer, int mode);
        }
        public static void Copy(nint renderer, nint texture, Rectangle<int>? src = null, Rectangle<float>? dest = null, double angle = 0, Point<float>? center = null)
        {
            var uSrc = stackalloc int[4];
            *uSrc = src?.X ?? 0;
            *(uSrc+1) = src?.Y ?? 0;
            *(uSrc+2) = src?.Width ?? 0;
            *(uSrc+3) = src?.Height ?? 0;
            
            var uDest = stackalloc float[4];
            *uDest = dest?.X ?? 0;
            *(uDest+1) = dest?.Y ?? 0;
            *(uDest+2) = dest?.Width ?? 0;
            *(uDest+3) = dest?.Height ?? 0; 
            var uCenter = stackalloc float[2];
            *uCenter = center?.X ?? 0;
            *(uCenter+1) = center?.Y ?? 0;
            if(_RenderCopyExF(renderer, texture, src != null ? uSrc : (int*) 0, dest != null ? uDest : (float*) 0, angle, center != null ? uCenter : (float*) 0, 0) != 0) throw new SDLException("Failed to copy texture");
            [DllImport("SDL2", EntryPoint = "SDL_RenderCopyExF")]
            static extern int _RenderCopyExF(nint renderer, nint texture, int* src, float* dest, double angle, float* center, int flip);
        }
    }
    public static class Surface
    {
        public static void Free(nint surface)
        {
            _FreeSurface(surface);
            [DllImport("SDL2", EntryPoint = "SDL_FreeSurface")]
            static extern void _FreeSurface(nint surface);
        }
    }
    public static class Texture
    {
        public static nint CreateFromSurface(nint renderer, nint surface)
        {
            var ret = _CreateTextureFromSurface(renderer, surface);
            if(ret == nint.Zero) throw new SDLException("Failed to create texture");
            return ret;
            [DllImport("SDL2", EntryPoint = "SDL_CreateTextureFromSurface")]
            static extern nint _CreateTextureFromSurface(nint renderer, nint surface);
        }
        public static void Destroy(nint texture)
        {
            _DestroyTexture(texture);
            [DllImport("SDL2", EntryPoint = "SDL_DestroyTexture")]
            static extern void _DestroyTexture(nint texture);
        }
        public static void SetColorMod(nint texture, byte r, byte g, byte b)
        {
            if(_SetTextureColorMod(texture, r, g, b) != 0) throw new SDLException("Failed to set texture color");
            [DllImport("SDL2", EntryPoint = "SDL_SetTextureColorMod")]
            static extern int _SetTextureColorMod(nint texture, byte r, byte g, byte b);
        }
        public static void SetColorAlpha(nint texture, byte a)
        {
            if(_SetTextureAlphaMod(texture, a) != 0) throw new SDLException("Failed to set texture alpha");
            [DllImport("SDL2", EntryPoint = "SDL_SetTextureAlphaMod")]
            static extern int _SetTextureAlphaMod(nint texture, byte a);
        } 
        public static void SetBlendMode(nint texture, BlendMode mode)
        {
            if(_SetTextureBlendMode(texture, (int) mode) != 0) throw new SDLException("Failed to set texture blend mode");
            [DllImport("SDL2", EntryPoint = "SDL_SetTextureBlendMode")]
            static extern int _SetTextureBlendMode(nint texture, int a);
        }
    }
    public static class Event
    {
        public static bool Poll([NotNullWhen(true)] out IEvent? evt)
        {
            var uEvt = stackalloc byte[56];
            var ret = _PollEvent((nint)uEvt) == 1;
            if(!ret) return false;
            var type = *(EventType*)uEvt;
            evt = type switch
            {
                EventType.Quit => QuitEvent.FromData(uEvt),
                EventType.Window => WindowEvent.FromData(uEvt),
                _ => UnknownEvent.FromData(uEvt)
            };
            return true;
            [DllImport("SDL2", EntryPoint = "SDL_PollEvent")]
            static extern int _PollEvent(nint evt);
        }
    }
}