namespace Directionful.SDL.Util;

public readonly unsafe partial record struct Rectangle<T> where T : unmanaged
{
    internal void ToData(T* data)
    {
        *data = X;
        *(data+1) = Y;
        *(data+2) = Width;
        *(data+3) = Height;
    }
}
