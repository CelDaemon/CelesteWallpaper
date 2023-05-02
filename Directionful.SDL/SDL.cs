namespace Directionful.SDL;

public class SDL : IDisposable
{
    private readonly InitFlag _flags;
    private readonly Video.Video? _video;
    private bool _disposed;
    public SDL(InitFlag flags)
    {
        if(flags.HasFlag(InitFlag.Video)) flags |= InitFlag.Events;
        _flags = flags;
        Native.SDL.Init(flags);
        if(flags.HasFlag(InitFlag.Video)) _video = new();
    }
    public InitFlag Flags {get => _flags; }
    public Video.Video Video {get => _video ?? throw new Exception("Video subsystem is not initialized"); }

    public void Dispose()
    {
        if(_disposed) return;
        _disposed = true;
        GC.SuppressFinalize(this);
        Native.SDL.Quit();
    }
    public override string ToString()
    {
        return $"SDL ({_flags})";
    }
}