using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Directionful.SDL;

[DebuggerDisplay("SDL ({_flags})")]
public class SDL
{
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly InitFlag _flags;
    public SDL(InitFlag flags)
    {
        if(flags.HasFlag(InitFlag.Video)) flags |= InitFlag.Events;
        _flags = flags;
        Native.SDL.Init(flags);
    }
    public InitFlag Flags {get => _flags; }
}