namespace Mes.Shared.Contracts.ValueObjects;

public sealed record Quantity(double Value, string Unit)
{
}

public sealed record QuantityPerSecond(Quantity Quantity, TimeSpan Time)
{
}