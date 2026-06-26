namespace Mes.Shopfloor.Client.ProductionManagement.Resources;

internal sealed class ProductionUnitGroupModel
{
    public int Id { get; init; }
    public required string Name { get; init; }
    public required Guid StateGroupId { get; init; }
    public required Guid RejectGroupId { get; init; }
}