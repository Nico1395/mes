namespace Mes.Shopfloor.Client.ProductionManagement.Resources;

internal sealed class ProductionUnitModel
{
    public required Guid Id { get; init; }
    public required string Key { get; init; }
    public required string Name { get; init; }
    public required ProductionUnitTypeModel Type { get; init; }
    public required ProductionUnitGroupModel Group { get; init; }
}