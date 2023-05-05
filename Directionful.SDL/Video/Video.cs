using Directionful.SDL.Event;
using Directionful.SDL.Util;

namespace Directionful.SDL.Video;

public class Video : IDisposable
{
    public delegate void ClipboardUpdateHandler(ClipboardUpdateEvent evt);
    /// <remarks>
    ///   Don't rely on this event, it's not triggered on x11
    /// </remarks>
    public ClipboardUpdateHandler? OnClipboardUpdate;
    internal delegate void UnregisterWindow(uint id);
    private bool _disposed;
    private readonly Dictionary<uint, Window> _windows = new();
    private bool _screenSaverEnabled = true;

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
    ///
    /// <remarks>
    ///   SLOW, don't use this multiple times per frame
    /// </remarks>
    public string? Clipboard
    {
        set
        {
            if(_disposed) throw new ObjectDisposedException(nameof(Video));
            if(value != null) Native.SDL.Clipboard.SetText(value);
            else Native.SDL.Clipboard.ClearText();
        }
        get
        {
            if(_disposed) throw new ObjectDisposedException(nameof(Video));
            return Native.SDL.Clipboard.GetText();
        }
    }
    internal Window GetWindow(uint id) => _windows[id];


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

    internal void HandleEvent(ClipboardUpdateEvent evt)
    {
        OnClipboardUpdate?.Invoke(evt);
    }
}