using System.Runtime.InteropServices;

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
    public static string? GetError()
    {
        return Marshal.PtrToStringAnsi(_GetError());
        [DllImport("SDL2", EntryPoint = "SDL_GetError")]
        static extern nint _GetError();
    }
}