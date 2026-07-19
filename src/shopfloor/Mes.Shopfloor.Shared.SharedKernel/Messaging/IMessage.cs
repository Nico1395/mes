namespace Mes.Shopfloor.Shared.SharedKernel.Messaging;

public interface IMessage
{
    Guid Id { get; init; }
    DateTime OccurredAtUtc { get; init; }
}