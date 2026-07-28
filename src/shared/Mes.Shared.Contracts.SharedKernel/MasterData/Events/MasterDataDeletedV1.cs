using Mes.Library.RabbitMQ;
using Mes.Shared.Contracts.SharedKernel.Abstractions;

namespace Mes.Shared.Contracts.SharedKernel.MasterData.Events;

public sealed class MasterDataDeletedV1 : Message
{
    public required List<IMasterDataEntity> Data { get; init; }
    public string[]? ShopfloorKeys { get; init; }
}