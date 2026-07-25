namespace Mes.Libraries.RabbitMQ;

public interface IMessage
{
    Guid Id { get; init; }
    DateTime OccurredAtUtc { get; init; }
}