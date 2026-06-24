namespace Mes.Shopfloor.Shared.Messaging;

public interface IMessage
{
    Guid Id { get; init; }
    DateTime OccurredAtUtc { get; init; }
}