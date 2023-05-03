namespace Directionful.SDL.Util;

public readonly record struct Point<T>(T X, T Y) where T : unmanaged
{
    public static Point<int> FromData(nint data)
    {
        unsafe
        {
            var uData = (int*)data;
            return new Point<int>(*uData, *(uData+1));
        }
    }
}