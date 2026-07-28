namespace Mes.Shared.Contracts.SharedKernel.ValueObjects;

public sealed record Duration(TimeSpan Value, double DeviationSeconds)
{
    public static Duration Empty => new(TimeSpan.MinValue, 0);

    public bool IsWithinDeviation(Duration other)
    {
        return IsWithinDeviation(other.Value);
    }

    public bool IsWithinDeviation(TimeSpan timeSpan)
    {
        var difference = (Value - timeSpan).TotalSeconds;
        return difference >= -DeviationSeconds && difference <= DeviationSeconds;
    }

    public static Duration operator +(Duration a, Duration b)
    {
        return new Duration(a.Value + b.Value, (a.DeviationSeconds + b.DeviationSeconds) / 2);
    }

    public static Duration operator -(Duration a, Duration b)
    {
        return new Duration(a.Value - b.Value, (a.DeviationSeconds + b.DeviationSeconds) / 2);
    }

    public static Duration operator *(Duration a, double scalar)
    {
        return new Duration(TimeSpan.FromTicks((long)(a.Value.Ticks * scalar)), a.DeviationSeconds * scalar);
    }

    public static Duration operator /(Duration a, double scalar)
    {
        return new Duration(TimeSpan.FromTicks((long)(a.Value.Ticks / scalar)), a.DeviationSeconds / scalar);
    }

    public static bool operator <(Duration a, Duration b)
    {
        return a.Value < b.Value;
    }

    public static bool operator >(Duration a, Duration b)
    {
        return a.Value > b.Value;
    }

    public static bool operator <=(Duration a, Duration b)
    {
        return a.Value <= b.Value;
    }

    public static bool operator >=(Duration a, Duration b)
    {
        return a.Value >= b.Value;
    }
}