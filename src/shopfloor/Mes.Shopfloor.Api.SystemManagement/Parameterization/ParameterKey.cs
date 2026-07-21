namespace Mes.Shopfloor.Api.SystemManagement.Parameterization;

internal sealed class ParameterKey
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public required string Key { get; init; }
    public List<ParameterValue>? Values { get; init; }
}