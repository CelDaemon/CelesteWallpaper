namespace Directionful.SDL.Util;

public readonly partial record struct Point<T> where T : unmanaged
{
    static partial void FromData(ref Point<int> point, nint data)
    {
        unsafe
        {
            var uData = (int*) data;
            point = point with {X = *uData, Y = *(uData+1)};
        }
    }
}