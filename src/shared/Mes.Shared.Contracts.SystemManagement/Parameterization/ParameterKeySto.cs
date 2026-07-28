using Microsoft.AspNetCore.Components;

namespace Mes.Shared.Contracts.SystemManagement.Parameterization;

public sealed class ParameterKeySto
{
    public required Guid Id { get; init; }
    public required string Key { get; init; }
    public List<ParameterValue>? Values { get; init; }
}