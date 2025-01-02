using JitterGang.libs;

namespace JitterGang.Services.Jitter;

public class LeftRightJitter : BaseJitter
{
    private readonly int _strength;
    private int _currentDirection = 1;
    private int _moveCount;
    private const int MovesPerDirection = 10;
    private const int MicroStrength = 1;

    public LeftRightJitter(int strength)
    {
        _strength = strength;
    }

    public override void ApplyJitter(ref INPUT input)
    {
        int microMove = Math.Min(MicroStrength, _strength);
        input.Mi.Dx += microMove * _currentDirection;

        _moveCount++;
        if (_moveCount >= MovesPerDirection)
        {
            _currentDirection *= -1;
            _moveCount = 0;
        }
    }
}

public class CircleJitter : BaseJitter
{
    private readonly int _radius;
    private double _angle;
    private const double AngleIncrement = Math.PI / 2;

    public CircleJitter(int radius)
    {
        _radius = radius;
    }

    public override void ApplyJitter(ref INPUT input)
    {
        input.Mi.Dx += (int)(_radius * Math.Cos(_angle));
        input.Mi.Dy += (int)(_radius * Math.Sin(_angle));

        _angle += AngleIncrement;
        if (_angle >= 2 * Math.PI)
        {
            _angle -= 2 * Math.PI;
        }
    }
}

public class SmoothLeftRightJitter : BaseJitter
{
    private const int Radius = 2;
    private const double AngleIncrement = 0.09;
    private double _angle;

    public SmoothLeftRightJitter()
    {
        _angle = 0;
    }

    public override void ApplyJitter(ref INPUT input)
    {
        double deltaX = Radius * Math.Cos(_angle);
        double deltaY = Radius * Math.Sin(_angle);

        input.Mi.Dx += (int)Math.Round(deltaX);
        input.Mi.Dy += (int)Math.Round(deltaY);

        _angle += AngleIncrement;
        if (_angle >= 2 * Math.PI)
        {
            _angle -= 2 * Math.PI;
        }
    }
}

public class PullDownJitter : BaseJitter
{
    private int _strength;
    private int _accumulatedStrength;

    public PullDownJitter(int strength)
    {
        _strength = strength;
        _accumulatedStrength = 0;
    }

    public override void ApplyJitter(ref INPUT input)
    {
        float weakenedStrength = _strength / 60.0f;
        _accumulatedStrength += (int)(weakenedStrength * 100);

        if (_accumulatedStrength >= 100)
        {
            int pixelsToMove = _accumulatedStrength / 100;
            input.Mi.Dy += pixelsToMove;
            _accumulatedStrength %= 100;
        }
    }

    public void UpdateStrength(int newStrength)
    {
        _strength = newStrength;
    }
}