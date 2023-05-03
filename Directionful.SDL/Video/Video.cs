using Directionful.SDL.Event;
using Directionful.SDL.Util;

namespace Directionful.SDL.Video;

public class Video : IDisposable
{
    internal delegate void UnregisterWindow(uint id);
    private bool _disposed;
    private readonly Dictionary<uint, Window> _windows = new();
    private bool _screenSaverEnabled = true;
    private string? _clipboard;
    public Video()
    {
        _clipboard = Native.SDL.Clipboard.GetText();
    }

    public Window CreateWindow(string title, Rectangle<int> location, WindowFlag flags)
    {
        if(_disposed) throw new ObjectDisposedException(nameof(Video));
        var window = new Window(id => _windows.Remove(id), title, location, flags);
        _windows[window.ID] = window;
        return window;
    }
    public bool ScreenSaverEnabled
    {
        get => _screenSaverEnabled;
        set
        {
            if(_disposed) throw new ObjectDisposedException(nameof(Video));
            if (value) Native.SDL.ScreenSaver.Enable();
            else Native.SDL.ScreenSaver.Disable();
            _screenSaverEnabled = value;
        }
    }
    public string? Clipboard
    {
        set
        {
            if(_disposed) throw new ObjectDisposedException(nameof(Video));
            if(value != null) Native.SDL.Clipboard.SetText(value);
            else Native.SDL.Clipboard.ClearText();
            _clipboard = value;
        }
        get => _clipboard;
    }
    internal Window GetWindow(uint id) => _windows[id];

    internal void HandleEvent(ClipboardEvent _)
    {
        _clipboard = Native.SDL.Clipboard.GetText();
    }


    public void Dispose()
    {
        if(_disposed) return;
        _disposed = true;
        GC.SuppressFinalize(this);
        foreach(var window in _windows.Values)
        {
            window.Dispose();
        }
    }
}