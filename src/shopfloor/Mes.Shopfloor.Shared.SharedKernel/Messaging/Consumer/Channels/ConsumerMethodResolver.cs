using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace Mes.Shopfloor.Shared.SharedKernel.Messaging.Consumer.Channels;

public static class ConsumerMethodResolver
{
    private static readonly ConcurrentDictionary<Type, MethodInfo> _methodInfos = [];
    
    public static bool TryReflectMethod(Type messageType, [MaybeNullWhen(false)] out MethodInfo method)
    {
        method = null;

        if (_methodInfos.TryGetValue(messageType, out method))
            return true;

        var consumerType = typeof(IConsumer<>).MakeGenericType(messageType);
        method = consumerType.GetMethods().FirstOrDefault(m => m.Name == nameof(IConsumer<>.HandleAsync));
        if (method == null)
            return false;

        _methodInfos[messageType] = method;
        return true;
    }
}