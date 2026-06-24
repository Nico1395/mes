namespace Mes.Shopfloor.Shared.Messaging.Consumer.Configuration;

public sealed class ConsumerListeningChannelConfiguration(string exchange, string queue)
{
    internal List<string> RoutingKeysInternal { get; } = [];
    
    public string Queue { get; } = queue;
    public string Exchange { get; } = exchange;
    public IReadOnlyList<string> RoutingKeys => RoutingKeysInternal;
    public DeclareQueueOptions QueueOptions { get; internal set; } = new();
    public bool RequeueOnException { get; internal set; } = false;
    public ushort PrefetchCount { get; internal set; } = 32;
}