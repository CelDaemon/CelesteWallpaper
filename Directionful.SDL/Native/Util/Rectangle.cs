namespace Directionful.SDL.Util;

public unsafe readonly partial record struct Rectangle<T> where T : unmanaged
{
    public void ToData(T* data)
    {
        *data = X;
        *(data+1) = Y;
        *(data+2) = Width;
        *(data+3) = Height;
    }
}