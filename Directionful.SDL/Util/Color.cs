namespace Directionful.SDL.Util;

public readonly record struct Color(byte R, byte G, byte B, byte A)
{
    public readonly static Color Red = new(255, 0, 0, 255);
    public readonly static Color Green = new(0, 255, 0, 255);
    public readonly static Color Blue = new(0, 0, 255, 255);
    public readonly static Color White = new(255, 255, 255, 255);
    public readonly static Color Black = new(0, 0, 0, 255);
    public readonly static Color Purple = new(159, 0, 197, 255);
}