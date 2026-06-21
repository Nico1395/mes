namespace Mes.Shopfloor.Core.Messaging.Consumer.Configuration;

public sealed class DeclareQueueOptions
{
    public bool Durable { get; set; }
    public bool Exclusive { get; set; }
    public bool AutoDelete { get; set; }
    public bool NoWait { get; set; }
    public Dictionary<string, object?>? Arguments { get; set; }
}