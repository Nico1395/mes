namespace Mes.Shopfloor.Api.ProductionManagement.DataCollection.Requests.Http;

internal sealed class StateGroupHc
{
    public Guid Id { get; init; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public List<StateHc>? States { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}