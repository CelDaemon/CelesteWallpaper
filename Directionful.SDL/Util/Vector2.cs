using System.Numerics;

namespace Directionful.SDL.Util;

public readonly record struct Vector2<T>(T X, T Y) where T : unmanaged, IComparable<T>, IAdditionOperators<T, T, T>, IMultiplyOperators<T, T, T>, IMultiplyOperators<T, float, float>
{
    public bool OutOfBounds(Rectangle<T> bounds)
    {
        if(X.CompareTo(bounds.X) < 0) return true;
        if(X.CompareTo(bounds.X + bounds.Width) > 0) return true;
        if(Y.CompareTo(bounds.Y) < 0) return true;
        if(Y.CompareTo(bounds.Y + bounds.Height) > 0) return true;
        return false;
    }
    public float Length()
    {
        if(X is not float x) throw new NotSupportedException();
        if(Y is not float y) throw new NotSupportedException();
        return MathF.Sqrt(x * x + y * y);
    }
    public float Angle()
    {
        if(X is not float x) throw new NotSupportedException();
        if(Y is not float y) throw new NotSupportedException();
        return MathF.Atan2(y, x);
    }
    public static Vector2<T> operator *(Vector2<T> a, Vector2<T> b)
    {
        return new Vector2<T>(
            a.X * b.X,
            a.Y * b.Y
        );
    }
    public static Vector2<float> operator *(Vector2<T> a, float b)
    {
        return new Vector2<float>(
            a.X * b,
            a.Y * b
        );
    }
    public static Vector2<float> operator /(Vector2<T> a, int b)
    {
        if(a.X is not float x) throw new NotSupportedException();
        if(a.Y is not float y) throw new NotSupportedException();
        return new Vector2<float>(
            x / b,
            y / b
        );
    }
}