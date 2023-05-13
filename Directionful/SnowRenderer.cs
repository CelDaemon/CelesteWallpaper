using System.Diagnostics;
using Directionful.SDL.Util;
using Directionful.SDL.Video;
using Directionful.SDL.Video.Windowing;

namespace Directionful;

public class SnowRenderer
{
    public SnowRenderer(Window window, Renderer renderer)
    {
        _window = window;
        _renderer = renderer;
        Reset();
    }
    public void Reset()
    {
        for(var i = 0; i < _particles.Length; i++)
        {
            _particles[i].Reset(_window, _direction);
        }
    }
    public void Update()
    {
        for(var i = 0; i < _particles.Length; i++)
        {
            _particles[i].Position = new Vector2<float>(
                _particles[i].Position.X + _direction.X * _particles[i].Speed *  ((float) 1/1000*16), // fix timing
                _particles[i].Position.Y + MathF.Sin(_particles[i].Sin) * 100f * ((float) 1/1000*16)
            );
            _particles[i].Sin += 16;
            if(_particles[i].Position.OutOfBounds(new Rectangle<float>(
                -128,
                -128,
                _window.Location.Width + 128 * 2,
                _window.Location.Height + 128 * 2
            )))
            {
                _particles[i].Reset(_window, _direction);
            }
        }
        _timer += (float) 1/1000*16;
    }
    public void Render()
    {
        var num = MathUtil.Clamp(_direction.Length(), 0f, 20f);
        var num2 = 0f;
        var one = new Vector2<float>(1, 1);
        if(num > 1f)
        {
            num2 = _direction.Angle();
            one = new Vector2<float>(num, .2f + (1f - num / 20f) * .8f);
        }
        var num3 = _alpha * _particleAlpha;
        for(var i = 0; i < _particles.Length; i++)
        {
            var color = new Color(
                (byte) MathF.Round(_particles[i].Color.R * num3),
                (byte) MathF.Round(_particles[i].Color.G * num3),
                (byte) MathF.Round(_particles[i].Color.B * num3),
                (byte) MathF.Round(_particles[i].Color.A * num3)
            );
            // Debug.WriteLine($"Trying to draw centered snow: {particle.Position}, {color}, {one * particle.Scale}, {(num > 1f ? num2 : particle.Rotation)}");
            _renderer.DrawRectangle(new Rectangle<float>(_particles[i].Position.X - (one * _particles[i].Scale * 256).X / 2, _particles[i].Position.Y - (one * _particles[i].Scale * 256).Y / 2, (one * _particles[i].Scale * 256).X, (one * _particles[i].Scale * 256).Y), color, BlendMode.Blend);
        }
        var num4 = _timer * 32f % 1920;
        var num5 = _timer * 20f % 1080;
        var color2 = Color.White * (_alpha * _overlayAlpha);
        // Debug.WriteLine($"Trying to draw overlay: {new Rectangle<float>(-(int)num4, -(int)num5, 1920, 1080)}, {color2}");
    }
    private record struct Particle(float Scale, Vector2<float> Position, float Speed, float Sin, float Rotation, Color Color)
    {
        public void Reset(Window window, Vector2<float> direction)
        {
            var num = MathF.Pow(Random.Shared.NextSingle(), 4);
            Scale = MathUtil.Map(num, 0f, 1f, 0.05f, 0.8f);
            Speed = Scale * (Random.Shared.Next(5000 - 2500) + 2500);
            if (direction.X < 0f)
            {
                Position = new Vector2<float>(window.Location.Width + 128, Random.Shared.NextSingle() * window.Location.Height);
            } else if (direction.X > 0f)
            {
                Position = new Vector2<float>(-128f, Random.Shared.NextSingle() * window.Location.Height);
            } else if (direction.Y > 0f)
            {
                Position = new Vector2<float>(Random.Shared.NextSingle() * window.Location.Width, -128f);
            } else if (direction.Y < 0f)
            {
                Position = new Vector2<float>(Random.Shared.NextSingle() * window.Location.Width, window.Location.Height + 128);
            }
            else
            {
                Position = new Vector2<float>(Random.Shared.NextSingle() * window.Location.Width, Random.Shared.NextSingle() * window.Location.Height);
            }
            Sin = Random.Shared.NextSingle() * 6.2831855f;
            Rotation = Random.Shared.NextSingle() * 6.2831855f;
            Color = Color.Lerp(Color.White, Color.Transparent, num * 0.8f);
        }
    }
    private readonly Window _window;
    private readonly float _alpha = 1f;
    private readonly float _particleAlpha = 1f;
    private readonly Vector2<float> _direction = new(-1f, 0f);
    private float _timer;
    private readonly float _overlayAlpha = .45f;
    private readonly Particle[] _particles = new Particle[50];
    private readonly Renderer _renderer;
}