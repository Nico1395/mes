namespace Mes.Libraries.RabbitMQ.Consumer;

public enum ConsumerResultCode
{
    Ack = 0,
    Nack = 1,
    NackRequeue = 2,
}