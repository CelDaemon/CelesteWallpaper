using System.Runtime.CompilerServices;

namespace Directionful.SDL;

public class SDL
{
    public SDL()
    {
        Native.SDL.Init(InitFlag.Video);
    }
}