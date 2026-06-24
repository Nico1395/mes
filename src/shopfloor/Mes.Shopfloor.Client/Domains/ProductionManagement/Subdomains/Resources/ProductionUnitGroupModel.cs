namespace Mes.Shopfloor.Terminal.Core.Domains.ProductionManagement.Subdomains.Resources;

internal sealed class ProductionUnitGroupModel
{
    public int Id { get; init; }
    public required string Name { get; init; }
    public required Guid StateGroupId { get; init; }
    public required Guid RejectGroupId { get; init; }
}