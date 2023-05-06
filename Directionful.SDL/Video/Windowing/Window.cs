using System.Runtime.Versioning;
using Directionful.SDL.Event;
using Directionful.SDL.Event.Windowing;
using Directionful.SDL.Native.Enum;
using Directionful.SDL.Util;

namespace Directionful.SDL.Video.Windowing;

public class Window : IDisposable
{
    public delegate HitTestResult HitTestHandler(Window window, Point<int> point, nint data);
    public Window(VideoSystem video, string title, Rectangle<int> location, bool resizable = true, bool borderless = false, bool alwaysOnTop = false, bool hidden = false, DisplayState displayState = default, FullscreenState fullscreenState = default)
    {
        _video = video;
        _title = title;
        _location = location;
        _resizable = resizable;
        _borderless = borderless;
        _alwaysOnTop = alwaysOnTop;
        _hidden = hidden;
        _displayState = displayState;
        var flags = WindowFlag.None;
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
        flags |= fullscreenState switch
        {
            FullscreenState.Fullscreen => WindowFlag.Fullscreen,
            FullscreenState.BorderlessFullscreen => WindowFlag.FullscreenDesktop,
            _ => 0
        };
        _handle = Native.SDL.Window.Create(title, location, flags);
        _id = Native.SDL.Window.GetID(_handle);
        _uHitTest = InternalHitTest;

        video.RegisterWindow(this);

        Renderer = new Renderer(_handle, -1, true);
    }
    public void Dispose()
    {
        if (_disposed) return;
        _disposed = true;
        GC.SuppressFinalize(this);
        Renderer.Dispose();
        _video.UnregisterWindow(this);
        Native.SDL.Window.Destroy(_handle);
    }
    public void Flash(bool untilFocussed)
    {
        if (_disposed) throw new ObjectDisposedException(nameof(Window));
        Native.SDL.Window.Flash(_handle, untilFocussed ? FlashOperation.UntilFocussed : FlashOperation.Briefly);
    }
    public void CancelFlash()
    {
        if (_disposed) throw new ObjectDisposedException(nameof(Window));
        Native.SDL.Window.Flash(_handle, FlashOperation.Cancel);
    }
    public string Title
    {
        get => _title;
        set
        {
            if (_disposed) throw new ObjectDisposedException(nameof(Window));
            if(_title == value) return;
            Native.SDL.Window.SetTitle(_handle, value);
            _title = value;
        }
    }
    public Rectangle<int> Location
    {
        get => _location;
        set
        {
            if (_disposed) throw new ObjectDisposedException(nameof(Window));
            if (_location == value) return;
            if (_location.X != value.X || _location.Y != value.Y) Native.SDL.Window.SetPosition(_handle, value.X, value.Y);
            if (_location.Width != value.Width || _location.Height != value.Height) Native.SDL.Window.SetSize(_handle, value.Width, value.Height);
            _location = value;
        }
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
            if (_disposed) throw new ObjectDisposedException(nameof(Window));
            if (_borderless == value) return;
            Native.SDL.Window.SetBordered(_handle, !value);
            _borderless = value;
        }
    }
    public bool AlwaysOnTop
    {
        get => _alwaysOnTop;
        set
        {
            if (_disposed) throw new ObjectDisposedException(nameof(Window));
            if (_alwaysOnTop == value) return;
            Native.SDL.Window.SetAlwaysOnTop(_handle, value);
            _alwaysOnTop = value;
        }
    }
    public bool Hidden
    {
        get => _hidden;
        set
        {
            if (_disposed) throw new ObjectDisposedException(nameof(Window));
            if (_hidden == value) return;
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
            if (_disposed) throw new ObjectDisposedException(nameof(Window));
            if (_displayState == value) return;
            switch (value)
            {
                case DisplayState.Maximized:
                    Native.SDL.Window.Maximize(_handle);
                    break;
                case DisplayState.Minimized:
                    Native.SDL.Window.Minimize(_handle);
                    break;
                case DisplayState.Normal:
                    Native.SDL.Window.Restore(_handle);
                    break;
            }
            _displayState = value;
        }
    }
    public FullscreenState FullscreenState
    {
        get => _fullscreenState;
        set
        {
            if (_disposed) throw new ObjectDisposedException(nameof(Window));
            if (_fullscreenState == value) return;
            var flags = value switch
            {
                FullscreenState.Fullscreen => WindowFlag.Fullscreen,
                FullscreenState.BorderlessFullscreen => WindowFlag.Borderless,
                _ => WindowFlag.None
            };
            Native.SDL.Window.SetFullscreen(_handle, flags);
            _fullscreenState = value;
        }
    }
    public float Opacity
    {
        get => _opacity;
        set
        {
            if (_disposed) throw new ObjectDisposedException(nameof(Window));
            if (_opacity == value) return;
            Native.SDL.Window.SetOpacity(_handle, value);
            _opacity = value;
        }
    }
    [SupportedOSPlatform("Windows")]
    public HitTestHandler? HitTest
    {
        get => _hitTest;
        set
        {
            if (_disposed) throw new ObjectDisposedException(nameof(Window));
            if (_hitTest == value) return;
            if (value != null) Native.SDL.Window.SetHitTest(_handle, _uHitTest, nint.Zero);
            else Native.SDL.Window.SetHitTest(_handle, null, 0);
            _hitTest = value;
        }
    }
    public uint ID
    {
        get => _id;
    }
    public Renderer Renderer {get; init;}
    internal void HandleEvent(WindowEvent evt)
    {
        switch (evt.Type)
        {
            case WindowEventType.Minimized:
                _displayState = DisplayState.Minimized;
                break;
            case WindowEventType.Maximized:
                _displayState = DisplayState.Maximized;
                break;
            case WindowEventType.Restored:
                _displayState = DisplayState.Normal;
                break;
            case WindowEventType.Moved:
                if (evt.Data1 != _location.X || evt.Data2 != _location.Y) _location = _location with { X = evt.Data1, Y = evt.Data2 };
                break;
            case WindowEventType.SizeChanged:
            case WindowEventType.Resized:
                if (evt.Data1 != _location.Width || evt.Data2 != _location.Height) _location = _location with { Width = evt.Data1, Height = evt.Data2 };
                break;
        }
    }
    private readonly VideoSystem _video;
    private readonly nint _handle;
    private readonly uint _id;
    private readonly Native.SDL.Window.HitTestHandler _uHitTest;
    private bool _disposed;
    private string _title;
    private Rectangle<int> _location;
    private bool _resizable;
    private bool _borderless;
    private bool _alwaysOnTop;
    private bool _hidden;
    private DisplayState _displayState;
    private FullscreenState _fullscreenState;
    private float _opacity = 1;
    private HitTestHandler? _hitTest;
    private int InternalHitTest(nint window, nint area, nint data)
    {
        return (int)_hitTest!.Invoke(this, Point<int>.FromData(area), data);
    }
}