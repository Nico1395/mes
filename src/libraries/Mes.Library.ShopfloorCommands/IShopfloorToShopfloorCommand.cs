namespace Mes.Library.ShopfloorCommands;

public interface IShopfloorToShopfloorCommand : IShopfloorCommand
{
    string SenderShopfloorKey { get; }
}