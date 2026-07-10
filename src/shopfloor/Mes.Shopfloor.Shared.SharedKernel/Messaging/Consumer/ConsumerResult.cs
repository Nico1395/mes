namespace Mes.Shopfloor.Shared.SharedKernel.Messaging.Consumer;

public sealed class ConsumerResult(ConsumerResultCode code)
{
    public ConsumerResultCode Code { get; private set; } = code;
    
    public static ConsumerResult Ack()
    {
        return new ConsumerResult(ConsumerResultCode.Ack);
    }

    public static ConsumerResult Nack()
    {
        return new ConsumerResult(ConsumerResultCode.Nack);
    }

    public static ConsumerResult NackRequeue()
    {
        return new ConsumerResult(ConsumerResultCode.NackRequeue);
    }

    public ConsumerResult Combine(ConsumerResult result)
    {
        if (result.Code > Code)
            Code = result.Code;

        return this;
    }
}