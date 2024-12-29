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
    private const int BaseRadius = 2;
    private const double BaseAngleIncrement = 0.09;
    private double _angle;
    private double _previousDeltaX;
    private double _previousDeltaY;
    private const double SmoothingFactor = 0.5;
    private const double AdaptiveRadiusFactor = 0.1;
    private double _dynamicRadius = BaseRadius;

    public SmoothLeftRightJitter()
    {
        _angle = 0;
    }

    public override void ApplyJitter(ref INPUT input)
    {
        _dynamicRadius = BaseRadius + AdaptiveRadiusFactor * (Math.Abs(_previousDeltaX) + Math.Abs(_previousDeltaY));

        double currentDeltaX = _dynamicRadius * Math.Cos(_angle);
        double currentDeltaY = _dynamicRadius * Math.Sin(_angle);

        double smoothDeltaX = (currentDeltaX * SmoothingFactor) + (_previousDeltaX * (1 - SmoothingFactor));
        double smoothDeltaY = (currentDeltaY * SmoothingFactor) + (_previousDeltaY * (1 - SmoothingFactor));

        input.Mi.Dx += (int)Math.Round(smoothDeltaX);
        input.Mi.Dy += (int)Math.Round(smoothDeltaY);

        _previousDeltaX = smoothDeltaX;
        _previousDeltaY = smoothDeltaY;

        double adaptiveAngleIncrement = BaseAngleIncrement + 0.01 * Math.Sin(_angle);
        _angle += adaptiveAngleIncrement;

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