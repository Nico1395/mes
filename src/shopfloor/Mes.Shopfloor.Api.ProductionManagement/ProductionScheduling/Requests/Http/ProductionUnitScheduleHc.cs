namespace Mes.Shopfloor.Api.ProductionManagement.ProductionScheduling.Requests.Http;

internal sealed class ProductionUnitScheduleHc
{
    public Guid Id { get; init; }
    public required Guid ProductionUnitId { get; init; }
    public List<ProductionUnitTaskHc>? Tasks { get; init; }
}