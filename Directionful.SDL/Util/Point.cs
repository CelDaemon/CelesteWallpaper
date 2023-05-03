namespace Directionful.SDL.Util;

public readonly partial record struct Point<T>(T X, T Y) where T : unmanaged
{
    public static Point<int> FromData(nint data)
    {
        var point = default(Point<int>);
        FromData(ref point, data);
        return point;
    }
    static partial void FromData(ref Point<int> point, nint data);
}