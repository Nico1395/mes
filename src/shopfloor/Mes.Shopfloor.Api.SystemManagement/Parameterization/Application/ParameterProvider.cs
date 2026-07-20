using Mes.Shopfloor.Api.SharedKernel.Application.Parameterization;
using Mes.Shopfloor.Api.SharedKernel.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Mes.Shopfloor.Api.SystemManagement.Parameterization.Application;

internal sealed class ParameterProvider(
    IOptions<AppOptions> appOptions,
    DbContext context) : IParameterProvider
{
    public async Task<Dictionary<string, TValue?>> GetAsync<TValue>(IEnumerable<string> parameterKeys, CancellationToken cancellationToken)
    {
        InvalidConfigurationException.ThrowIfNull(appOptions.Value.ShopfloorKey);

        var values = await context.GetParameterValuesForKeys(parameterKeys, appOptions.Value.ShopfloorKey, cancellationToken);
        return values.ToDictionary(k => k.Key, k => k.Value.GetValue<TValue>());
    }
}