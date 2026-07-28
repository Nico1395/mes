namespace Mes.Shared.Contracts.SystemManagement.Parameterization;

public sealed class ParameterValueSto
{
    public required string ParameterKey { get; init; }
    public required string ShopfloorKey { get; init; }
    public required string SerializedValue { get; init; }
    public required DateTime CreatedAt { get; init; }
    public required DateTime UpdatedAt { get; init; }
}