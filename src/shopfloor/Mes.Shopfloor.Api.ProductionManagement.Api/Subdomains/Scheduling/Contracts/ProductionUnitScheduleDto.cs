namespace Mes.Shopfloor.Api.ProductionManagement.Api.Subdomains.Scheduling.Contracts;

internal sealed class ProductionUnitScheduleDto
{
    public Guid Id { get; init; }
    public required Guid ProductionUnitId { get; init; }
    public List<ProductionUnitTaskDto>? Tasks { get; init; }
}