namespace Mes.Shopfloor.Shared.SharedKernel.Messaging.Consumer.Configuration;

public sealed class DeclareQueueOptions
{
    public bool Durable { get; set; } = true;
    public bool Exclusive { get; set; } = false;
    public bool AutoDelete { get; set; } = false;
    public bool NoWait { get; set; }
    public Dictionary<string, object?>? Arguments { get; set; }
}