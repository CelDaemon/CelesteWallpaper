using System.Runtime.InteropServices;
using System.Text;

namespace Directionful.SDL.Native;

internal static unsafe class SDLImage
{
    public static void Init()
    {
        if(_Init(0) != 0) throw new SDLException("Failed to initialize SDL image library");
        [DllImport("SDL2_image", EntryPoint = "IMG_Init")]
        static extern int _Init(int flags);
    }
    public static nint Load(string path)
    {
        var uPathLength = Encoding.UTF8.GetMaxByteCount(path.Length) + 1;
        var uPath = stackalloc byte[uPathLength];
        fixed (char* pathPtr = path)
        {
            Encoding.UTF8.GetBytes(pathPtr, path.Length, uPath, uPathLength - 1);
        }
        *(uPath + uPathLength - 1) = 0;
        var ret = _Load((nint)uPath);
        if(ret == nint.Zero) throw new SDLException("Failed to load image");
        return ret;
        [DllImport("SDL2_image", EntryPoint = "IMG_Load")]
        static extern nint _Load(nint path);
    }
}