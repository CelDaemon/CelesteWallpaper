namespace Directionful.SDL.Enum;

public enum RendererFlag : uint
{
    None = 0x00000000,
    Software = 0x00000001,
    Accelerated = 0x00000002,
    PresentVSync = 0x00000004,
    TargetTexture = 0x00000008
}