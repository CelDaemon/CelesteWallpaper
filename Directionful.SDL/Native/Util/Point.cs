namespace Directionful.SDL.Util;

public unsafe readonly partial record struct Point<T> where T : unmanaged
{
    public static Point<int> FromData(nint data)
    {
        var uData = (int*)data;
        return new Point<int>(*uData, *(uData + 1));
    }
}