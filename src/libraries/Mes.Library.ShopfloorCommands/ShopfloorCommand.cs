using Mes.Library.RabbitMQ;

namespace Mes.Library.ShopfloorCommands;

public abstract class ShopfloorCommand : Message, IShopfloorCommand
{
    public required string ReceiverShopfloorKey { get; init; }
}