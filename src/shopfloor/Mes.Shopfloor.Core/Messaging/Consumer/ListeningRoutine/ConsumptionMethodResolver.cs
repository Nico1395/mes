using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace Mes.Shopfloor.Core.Messaging.Consumer.ListeningRoutine;

public static class ConsumptionMethodResolver
{
    private static readonly ConcurrentDictionary<Type, MethodInfo> _methodInfos = [];
    
    public static bool TryReflectMethod(Type messageType, [MaybeNullWhen(false)] out MethodInfo method)
    {
        method = null;

        if (_methodInfos.TryGetValue(messageType, out method))
            return true;

        var consumptionType = typeof(IConsumption<>).MakeGenericType(messageType);
        method = consumptionType.GetMethods().FirstOrDefault(m => m.Name == nameof(IConsumption<>.HandleAsync));
        if (method == null)
            return false;

        _methodInfos[messageType] = method;
        return true;
    }
}