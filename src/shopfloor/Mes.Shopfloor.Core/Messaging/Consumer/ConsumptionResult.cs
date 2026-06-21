namespace Mes.Shopfloor.Core.Messaging.Consumer;

public sealed class ConsumptionResult(ConsumptionResultCode code)
{
    public ConsumptionResultCode Code { get; private set; } = code;
    
    public static ConsumptionResult Ack()
    {
        return new ConsumptionResult(ConsumptionResultCode.Ack);
    }

    public static ConsumptionResult Nack()
    {
        return new ConsumptionResult(ConsumptionResultCode.Nack);
    }

    public static ConsumptionResult NackRequeue()
    {
        return new ConsumptionResult(ConsumptionResultCode.NackRequeue);
    }

    public ConsumptionResult Combine(ConsumptionResult result)
    {
        if (result.Code > Code)
            Code = result.Code;

        return this;
    }
}