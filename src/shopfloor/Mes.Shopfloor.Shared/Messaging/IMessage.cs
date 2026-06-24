namespace Mes.Shopfloor.Core.Messaging;

public interface IMessage
{
    Guid Id { get; init; }
    DateTime OccurredAtUtc { get; init; }
}