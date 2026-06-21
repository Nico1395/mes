namespace Mes.Shopfloor.Core.Contracts;

public abstract class EventBase
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public DateTime MessageTimestamp { get; init; } = DateTime.UtcNow;
}