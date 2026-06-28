namespace Mes.Shopfloor.Api.ProductionManagement.Subdomains.DataCollection.Contracts;

internal sealed class StateDto
{
    public Guid Id { get; init; }
    public required Guid StateGroupId { get; init; }
    public StateGroupDto? StateGroup { get; init; }
    public int Order { get; init; }
    public bool IsIdle { get; init; }
    public bool IsProductive { get; init; }
    public required string Code { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public required string Color { get; init; }
}