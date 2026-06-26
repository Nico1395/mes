namespace Mes.Shopfloor.Client.ProductionManagement.DataCollection;

internal sealed class StateGroupModel
{
    public Guid Id { get; init; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public required List<StateModel> States { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public StateModel? GetIdleState()
    {
        return States.FirstOrDefault(s => s.IsIdle);
    }

    public StateModel? GetFirstProductiveState()
    {
        return States.OrderBy(s => s.Order).FirstOrDefault(s => s.IsProductive);
    }
}