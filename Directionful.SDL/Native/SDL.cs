using System.Runtime.InteropServices;
using Directionful.SDL.Native.Flag;

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
    public static class Error
    {
        public static string? Get()
        {
            var ret = Marshal.PtrToStringAnsi(_GetError());
            if(string.IsNullOrEmpty(ret)) return null;
            return ret;
            [DllImport("SDL2", EntryPoint = "SDL_GetError")]
            static extern nint _GetError();
        }
    }
}