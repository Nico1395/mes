using Mes.Shopfloor.Api.SharedKernel.Domain;

namespace Mes.Shopfloor.Api.ProductionManagement.DataCollection;

internal sealed class RejectGroup : ITimestamped
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public required string Name { get; set; }
    public string? Description { get; set; }
    public List<Reject>? Rejects { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}