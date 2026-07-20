using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using Mes.Shopfloor.Api.SharedKernel.Domain.Abstractions.Timestamped;

namespace Mes.Shopfloor.Api.SystemManagement.Parameterization;

internal sealed class ParameterValue : ITimestamped
{
    public required string ParameterKey { get; init; }
    public required string ShopfloorKey { get; init; }
    public required string SerializedValue { get; init; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public static ParameterValue Create(string parameterKey, string shopfloorKey, string value)
    {
        return new ParameterValue
        {
            ParameterKey = parameterKey,
            ShopfloorKey = shopfloorKey,
            SerializedValue = value
        };
    }

    public object? GetValue(Type valueType)
    {
        return JsonSerializer.Deserialize(SerializedValue, valueType);
    }

    public TValue? GetValue<TValue>()
    {
        return GetValue(typeof(TValue)) is TValue value ? value : default;
    }

    public object GetValueOrDefault(Type valueType, object defaultValue)
    {
        return GetValue(valueType) ?? defaultValue;
    }

    public TValue GetValueOrDefault<TValue>(TValue defaultValue)
    {
        return GetValue<TValue>() ?? defaultValue;
    }

    public bool TryGetValue(Type valueType, [NotNullWhen(true)] out object? value)
    {
        return (value = GetValue(valueType)) != null;
    }

    public bool TryGetValue<TValue>([NotNullWhen(true)] out TValue? tValue)
    {
        tValue = default;

        if (!TryGetValue(typeof(TValue), out var value) || value is not TValue casted)
            return false;
        
        tValue = casted;
        return true;
    }
}