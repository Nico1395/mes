namespace Mes.Shopfloor.Api.ProductionManagement.Subdomains.DataCollection.Contracts;

internal sealed class RejectDto
{
    public Guid Id { get; init; }
    public required Guid RejectGroupId { get; init; }
    public RejectGroupDto? RejectGroup { get; init; }
    public required int Order { get; init; }
    public required string Code { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public required string Color { get; init; }
}