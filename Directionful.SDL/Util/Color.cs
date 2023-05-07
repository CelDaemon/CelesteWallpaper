namespace Directionful.SDL.Util;

public readonly record struct Color(byte R, byte G, byte B, byte A = 255)
{
    public static readonly Color White = new(255, 255, 255);
    public static readonly Color Black = new(0, 0, 0);
    public static readonly Color Red = new(255, 0, 0);
    public static readonly Color Green = new(0, 255, 0);
    public static readonly Color Blue = new (0, 0, 255);
    public static readonly Color Purple = new(159, 0, 197);
}