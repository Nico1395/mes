namespace Mes.Library.ShopfloorCommands;

public abstract class ShopfloorCommand : IShopfloorCommand
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public required string ShopfloorKey { get; init; }
    public required string Key { get; init; }
    public DateTime SentAt { get; init; } = DateTime.UtcNow;
}