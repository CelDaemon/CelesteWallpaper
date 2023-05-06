using Directionful.SDL.Native.Flag;
using Directionful.SDL.Util;

namespace Directionful.SDL.Video.Windowing;

public class Window : IDisposable
{
    public Window(string title, Rectangle<int> location, bool resizable = true, bool borderless = false, bool alwaysOnTop = false, bool hidden = false, DisplayState displayState = default)
    {
        var flags = WindowFlag.None;
        _resizable = resizable;
        _borderless = borderless;
        _alwaysOnTop = alwaysOnTop;
        _hidden = hidden;
        _displayState = displayState;
        if (resizable) flags |= WindowFlag.Resizable;
        if (borderless) flags |= WindowFlag.Borderless;
        if (alwaysOnTop) flags |= WindowFlag.AlwaysOnTop;
        if (hidden) flags |= WindowFlag.Hidden;
        else flags |= WindowFlag.Shown;
        flags |= displayState switch
        {
            DisplayState.Maximized => WindowFlag.Maximized,
            DisplayState.Minimized => WindowFlag.Minimized,
            _ => 0
        };
        _handle = Native.SDL.Window.Create(title, location, flags);
    }
    public void Dispose()
    {
        if (_disposed) return;
        _disposed = true;
        GC.SuppressFinalize(this);
        Native.SDL.Window.Destroy(_handle);
    }
    public bool Resizable
    {
        get => _resizable;
        set
        {
            if (_disposed) throw new ObjectDisposedException(nameof(Window));
            if (_resizable == value) return;
            Native.SDL.Window.SetResizable(_handle, value);
            _resizable = value;
        }
    }
    public bool Borderless
    {
        get => _borderless;
        set
        {
            if(_disposed) throw new ObjectDisposedException(nameof(Window));
            if(_borderless == value) return;
            Native.SDL.Window.SetBordered(_handle, !value);
            _borderless = value;
        }
    }
    public bool AlwaysOnTop
    {
        get => _alwaysOnTop;
        set
        {
            if(_disposed) throw new ObjectDisposedException(nameof(Window));
            if(_alwaysOnTop == value) return;
            Native.SDL.Window.SetAlwaysOnTop(_handle, value);
            _alwaysOnTop = value;
        }
    }
    public bool Hidden
    {
        get => _hidden;
        set
        {
            if(_disposed) throw new ObjectDisposedException(nameof(Window));
            if(_hidden == value) return;
            if (value) Native.SDL.Window.Hide(_handle);
            else Native.SDL.Window.Show(_handle);
            _hidden = value;
        }
    }
    public DisplayState DisplayState
    {
        get => _displayState;
        set
        {
            if(_disposed) throw new ObjectDisposedException(nameof(Window));
            if(_displayState == value) return;
            switch (value)
            {
                case DisplayState.Maximized:
                    Native.SDL.Window.Maximize(_handle);
                    break;
                case DisplayState.Minimized:
                    Native.SDL.Window.Minimize(_handle);
                    break;
                case DisplayState.None:
                    Native.SDL.Window.Restore(_handle);
                    break;
            }
            _displayState = value;
        }
    }

    private readonly nint _handle;
    private bool _disposed;
    private bool _resizable;
    private bool _borderless;
    private bool _alwaysOnTop;
    private bool _hidden;
    private DisplayState _displayState;
}