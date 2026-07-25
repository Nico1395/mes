using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;

namespace Mes.Library.RabbitMQ.Consumer.Channels;

public static class MessageTypeResolver
{
    private static readonly ConcurrentDictionary<string, Type> _messageTypes = [];

    public static bool TryResolveType(string? typeName, [MaybeNullWhen(false)] out Type type)
    {
        type = null;
        if (string.IsNullOrWhiteSpace(typeName))
            return false;

        if (_messageTypes.TryGetValue(typeName, out type))
            return true;
            
        type = Type.GetType(typeName);
        if (type == null)
            return false;

        _messageTypes[typeName] = type;
        return true;
    }
}