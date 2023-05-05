namespace Directionful.SDL.Util;

public readonly record struct Rectangle<T>(T X, T Y, T Width, T Height) where T : unmanaged;