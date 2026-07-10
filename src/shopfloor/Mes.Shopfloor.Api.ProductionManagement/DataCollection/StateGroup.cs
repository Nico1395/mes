namespace Mes.Shopfloor.Api.ProductionManagement.DataCollection;

internal sealed class StateGroup
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public required string Name { get; set; }
    public string? Description { get; set; }
    public List<State>? States { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public bool AreStatesValid()
    {
        if (States == null)
            return false;

        var idleStateCount = States.Count(s => s.IsIdle);
        var productiveStateCount = States.Count(s => s.IsProductive);

        return idleStateCount == 1 && productiveStateCount >= 1;
    }
}