namespace Mes.Shopfloor.Api.ProductionManagement.DataCollection.Requests.Http;

internal sealed class RejectGroupHc
{
    public Guid Id { get; init; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public List<RejectHc>? Rejects { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}