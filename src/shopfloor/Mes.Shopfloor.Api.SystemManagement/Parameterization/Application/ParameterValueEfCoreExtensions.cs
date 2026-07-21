using Microsoft.EntityFrameworkCore;

namespace Mes.Shopfloor.Api.SystemManagement.Parameterization.Application;

internal static class ParameterValueEfCoreExtensions
{
    public static Task<Dictionary<string, ParameterValue>> GetParameterValuesForKeys(this DbContext context, IEnumerable<string> parameterKeys, string shopfloorKey, CancellationToken cancellationToken)
    {
        return context
            .Set<ParameterValue>()
            .Where(v => v.ShopfloorKey == shopfloorKey && parameterKeys.Contains(v.ParameterKey))
            .ToDictionaryAsync(v => v.ParameterKey, v => v, cancellationToken);
    }
}