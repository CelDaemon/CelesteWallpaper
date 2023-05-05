namespace Directionful.SDL.Util;

public readonly partial record struct Point<T>(T X, T Y) where T : unmanaged;