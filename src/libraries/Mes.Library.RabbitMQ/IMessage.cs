namespace Mes.Library.RabbitMQ;

public interface IMessage
{
    Guid Id { get; init; }
    DateTime OccurredAtUtc { get; init; }
}