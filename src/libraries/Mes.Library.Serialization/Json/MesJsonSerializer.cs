using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

namespace Mes.Library.Serialization.Json;

public static class MesJsonSerializer
{
    private static JsonSerializerOptions Options { get; } = new()
    {
        TypeInfoResolver = CreateTypeInfoResolver(),
    };

    public static IJsonTypeInfoResolver CreateTypeInfoResolver()
    {
        return new DefaultJsonTypeInfoResolver
        {
            Modifiers =
            {
                static typeInfo =>
                {
                    if (typeInfo.Kind == JsonTypeInfoKind.Object)
                    {
                        typeInfo.PolymorphismOptions = new JsonPolymorphismOptions
                        {
                            TypeDiscriminatorPropertyName = "$type",
                            UnknownDerivedTypeHandling = JsonUnknownDerivedTypeHandling.FallBackToBaseType
                        };
                    }
                }
            }
        };
    }

    public static string Serialize(object? value)
    {
        return JsonSerializer.Serialize(value, Options);
    }

    public static T? Deserialize<T>(string? json)
    {
        return string.IsNullOrWhiteSpace(json)
            ? default
            : JsonSerializer.Deserialize<T>(json, Options);
    }
}