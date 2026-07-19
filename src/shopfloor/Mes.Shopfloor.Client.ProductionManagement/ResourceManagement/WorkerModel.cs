namespace Mes.Shopfloor.Client.ProductionManagement.ResourceManagement;

internal sealed class WorkerModel
{
    public Guid Id { get; init; }
    public string? Number { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required Guid GroupId { get; set; }
    public required WorkerGroupModel Group { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}