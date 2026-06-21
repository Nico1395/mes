namespace Mes.Shopfloor.Core.Messaging.Consumer;

public enum ConsumptionResultCode
{
    Ack = 0,
    Nack = 1,
    NackRequeue = 2,
}