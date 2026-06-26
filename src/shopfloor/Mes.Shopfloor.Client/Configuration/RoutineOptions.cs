namespace Mes.Shopfloor.Client.Configuration;

public sealed class RoutineOptions
{
    public int IntervalMs { get; set; } = TimeSpan.FromSeconds(1).Milliseconds;
}