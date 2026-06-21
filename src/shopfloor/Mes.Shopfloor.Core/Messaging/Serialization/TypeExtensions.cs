namespace Mes.Shopfloor.Core.Messaging.Serialization;

public static class TypeExtensions
{
    public static string? GetIdentifiableName(this Type type)
    {
        if (!type.IsGenericType)
            return type.FullName;

        var genericTypeDefinition = type.GetGenericTypeDefinition();
        var genericArguments = string.Join(", ", type.GetGenericArguments().Select(a => a.GetIdentifiableName()));

        return $"{genericTypeDefinition.FullName}[[{genericArguments}]]";

    }
}