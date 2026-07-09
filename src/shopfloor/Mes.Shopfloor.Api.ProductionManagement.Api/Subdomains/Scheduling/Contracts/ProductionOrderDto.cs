namespace Mes.Shopfloor.Api.ProductionManagement.Api.Subdomains.Scheduling.Contracts;

internal sealed class ProductionOrderDto
{
    public Guid Id { get; init; }
    public required Guid ProductId { get; init; }
    public required string Name { get; init; }
    public string? Description { get; init; }
    public ProductionOrderProgressDto? Progress { get; init; }
    public int Priority { get; init; }
    public int State { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime UpdatedAt { get; init; }
}