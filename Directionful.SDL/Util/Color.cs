namespace Directionful.SDL.Util;

public readonly record struct Color(byte R, byte G, byte B, byte A = 255)
{
    public static readonly Color White = new(255, 255, 255);
    public static readonly Color Black = new(0, 0, 0);
    public static readonly Color Red = new(255, 0, 0);
    public static readonly Color Green = new(0, 255, 0);
    public static readonly Color Blue = new(0, 0, 255);
    public static readonly Color Purple = new(159, 0, 197);
    public static readonly Color Transparent = new(255, 255, 255, 0);

    public static Color Lerp(Color color1, Color color2, float t)
    {
        return new Color(
            (byte) MathF.Round((color1.R * (1 - t)) + (color2.R * t)),
            (byte) MathF.Round((color1.G * (1 - t)) + (color2.G * t)),
            (byte) MathF.Round((color1.B * (1 - t)) + (color2.B * t)),
            (byte) MathF.Round((color1.A * (1 - t)) + (color2.A * t))
        );
    }
    public static Color operator *(Color a, float b)
    {
        return new Color(
            (byte) MathF.Round(a.R * b),
            (byte) MathF.Round(a.G * b),
            (byte) MathF.Round(a.B * b),
            (byte) MathF.Round(a.A * b)
        );
    }
}