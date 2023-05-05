namespace Directionful.SDL.Util;

public readonly record struct Size<T>(T Width, T Height) where T : unmanaged;