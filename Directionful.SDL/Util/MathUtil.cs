namespace Directionful.SDL.Util;

public static class MathUtil
{
    public static float Map(float value, float max, float min, float newMax, float newMin)
        => (value - min) / (max - min) * (newMax - newMin) + newMin;
    public static float Clamp(float value, float min, float max)
    {
        if (value < min) return min;
        if (value > max) return max;
        return value;
    }
}