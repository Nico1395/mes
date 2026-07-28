namespace Mes.Shared.Contracts.SharedKernel.Abstractions.Timestamped;

public static class UpdatedExtensions
{
    public static bool UpdatedBefore(this IUpdated updated, DateTime pointInTime)
    {
        return updated.UpdatedAt < pointInTime;
    }

    public static bool UpdatedBefore(this IUpdated updated, IUpdated other)
    {
        return updated.UpdatedBefore(other.UpdatedAt);
    }

    public static bool UpdatedBeforeOrAt(this IUpdated updated, DateTime pointInTime)
    {
        return updated.UpdatedAt <= pointInTime;
    }

    public static bool UpdatedBeforeOrAt(this IUpdated updated, IUpdated other)
    {
        return updated.UpdatedBeforeOrAt(other.UpdatedAt);
    }

    public static bool UpdatedAfter(this IUpdated updated, DateTime pointInTime)
    {
        return updated.UpdatedAt > pointInTime;
    }

    public static bool UpdatedAfter(this IUpdated updated, IUpdated other)
    {
        return updated.UpdatedAfter(other.UpdatedAt);
    }

    public static bool UpdatedAfterOrAt(this IUpdated updated, DateTime pointInTime)
    {
        return updated.UpdatedAt >= pointInTime;
    }

    public static bool UpdatedAfterOrAt(this IUpdated updated, IUpdated other)
    {
        return updated.UpdatedAfterOrAt(other.UpdatedAt);
    }

    public static bool UpdatedAt(this IUpdated updated, DateTime pointInTime)
    {
        return updated.UpdatedAt == pointInTime;
    }

    public static bool UpdatedAt(this IUpdated updated, IUpdated other)
    {
        return updated.UpdatedAt(other.UpdatedAt);
    }

    public static void TouchUpdatedAt(this IUpdated updated, DateTime pointInTime)
    {
        updated.UpdatedAt = pointInTime;
    }

    public static void TouchUpdatedAt(this IUpdated updated)
    {
        updated.TouchUpdatedAt(DateTime.UtcNow);
    }

    public static TimeSpan TimeSinceUpdated(this IUpdated updated, DateTime pointInTime)
    {
        if (pointInTime < updated.UpdatedAt)
            return TimeSpan.Zero;

        return pointInTime - updated.UpdatedAt;
    }

    public static TimeSpan TimeSinceUpdated(this IUpdated updated)
    {
        return updated.TimeSinceUpdated(DateTime.UtcNow);
    }
}