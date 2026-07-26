namespace Mes.Library.RabbitMQ.Exceptions;

public sealed class InvalidMessageException(string message) : Exception(message)
{
    public static InvalidMessageException Create<TMessage>()
        where TMessage : IMessage
    {
        return new InvalidMessageException($"Invalid message: {typeof(TMessage).Name}");
    }

    public static void Throw<TMessage>()
        where TMessage : IMessage
    {
        throw Create<TMessage>();
    }
}