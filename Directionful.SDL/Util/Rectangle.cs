using System.Numerics;

namespace Directionful.SDL.Util;

public readonly partial record struct Rectangle<T>(T X, T Y, T Width, T Height) where T : unmanaged

{
    public static Rectangle<float> Centered(Vector2<float> origin, float width, float height)
    {
        return new Rectangle<float>(origin.X - width / 2, origin.Y - (height / 2), width, height);
    }
    public static Rectangle<float> Centered(Vector2<float> origin, Vector2<float> scale)
    {
        return Centered(origin, scale.X, scale.Y);
    }
}