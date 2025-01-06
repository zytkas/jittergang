using JitterGang.libs;

namespace JitterGang.Services.Jitter;

public class LeftRightJitter : BaseJitter
{
    private readonly int _strength;
    private int _currentDirection = 1;
    private int _moveCount;
    private const int MovesPerDirection = 8; // Reduced from 10 for faster direction changes
    private const double StrengthMultiplier = 0.5; // Multiplier for strength to make movements more noticeable

    public LeftRightJitter(int strength)
    {
        _strength = strength;
    }

    public override void ApplyJitter(ref INPUT input)
    {
        // Calculate movement based on strength
        int moveAmount = (int)Math.Ceiling(_strength * StrengthMultiplier);
        input.Mi.Dx += moveAmount * _currentDirection;

        _moveCount++;
        if (_moveCount >= MovesPerDirection)
        {
            _currentDirection *= -1; // Change direction
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
    private readonly double _baseStrength = 0.01; // Base movement per tick
    private int _strength;
    private double _accumulatedMovement;

    public PullDownJitter(int strength)
    {
        _strength = strength;
    }

    public override void ApplyJitter(ref INPUT input)
    {
        // Calculate movement this tick
        _accumulatedMovement += _strength * _baseStrength;

        // When we accumulate >= 1 pixel of movement, apply it
        if (_accumulatedMovement >= 1.0)
        {
            int pixelsToMove = (int)Math.Floor(_accumulatedMovement);
            input.Mi.Dy += pixelsToMove;
            _accumulatedMovement -= pixelsToMove;
        }
    }

    public void UpdateStrength(int newStrength)
    {
        _strength = newStrength;
    }
}