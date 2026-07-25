namespace Mes.Library.ShopfloorCommands;

public abstract class ShopfloorToShopfloorCommand : ShopfloorCommand, IShopfloorToShopfloorCommand
{
    public required string SenderShopfloorKey { get; init; }
}