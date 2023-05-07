namespace Directionful.SDL.Util;

public readonly record struct Color(byte R, byte G, byte B, byte A = 255)
{
    public static readonly Color Purple = new(159, 0, 197);
    public static readonly Color Black = new(0, 0, 0);
}