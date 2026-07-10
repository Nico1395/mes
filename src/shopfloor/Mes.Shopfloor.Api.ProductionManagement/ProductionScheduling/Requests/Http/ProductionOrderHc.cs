namespace Mes.Shopfloor.Api.ProductionManagement.ProductionScheduling.Requests.Http;

internal sealed class ProductionOrderHc
{
    public Guid Id { get; init; }
    public required Guid ProductId { get; init; }
    public required string Name { get; init; }
    public string? Description { get; init; }
    public ProductionOrderProgressHc? Progress { get; init; }
    public int Priority { get; init; }
    public int State { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime UpdatedAt { get; init; }
}