using Mes.Shopfloor.Api.SharedKernel.Domain;

namespace Mes.Shopfloor.Api.ProductionManagement.DataCollection;

internal sealed class PartConsumption : ICreated
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public required Guid PartId { get; init; }
    public required int Quantity { get; init; }
    public DateTime CreatedAt { get; set; }
}