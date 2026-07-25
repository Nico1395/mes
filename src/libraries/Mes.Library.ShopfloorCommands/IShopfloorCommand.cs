using Mes.Library.RabbitMQ;

namespace Mes.Library.ShopfloorCommands;

public interface IShopfloorCommand : IMessage
{
    string ReceiverShopfloorKey { get; }
}