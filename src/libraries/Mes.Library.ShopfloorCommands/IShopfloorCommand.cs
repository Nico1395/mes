namespace Mes.Library.ShopfloorCommands;

public interface IShopfloorCommand
{
    Guid Id { get; }
    string ShopfloorKey { get; }
    string Key { get; }
    DateTime SentAt { get; }
}