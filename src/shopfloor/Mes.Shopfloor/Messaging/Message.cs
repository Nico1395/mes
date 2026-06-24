namespace Mes.Shopfloor.Core.Messaging;

public abstract class Message : IMessage
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public DateTime OccurredAtUtc { get; init; } = DateTime.UtcNow;
}