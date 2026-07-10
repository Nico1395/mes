using Mes.Shopfloor.Api.SharedKernel.Domain;

namespace Mes.Shopfloor.Api.ProductionManagement.DataCollection;

internal sealed class MaterialConsumption : ICreated
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public required Guid MaterialId { get; init; }
    public required int Quantity { get; init; }
    public DateTime CreatedAt { get; set; }
}