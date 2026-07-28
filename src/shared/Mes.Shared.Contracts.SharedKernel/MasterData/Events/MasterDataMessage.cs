using Mes.Library.RabbitMQ;
using Mes.Shared.Contracts.SharedKernel.Abstractions;

namespace Mes.Shared.Contracts.SharedKernel.MasterData.Events;

public abstract class MasterDataMessage : Message
{
    public required Dictionary<string, IMasterData[]> Data { get; init; }
    public string[]? ShopfloorKeys { get; init; }
}