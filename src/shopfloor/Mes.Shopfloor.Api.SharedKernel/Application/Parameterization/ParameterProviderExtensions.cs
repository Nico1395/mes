namespace Mes.Shopfloor.Api.SharedKernel.Application.Parameterization;

public static class ParameterProviderExtensions
{
    public static async Task<TValue?> GetAsync<TValue>(this IParameterProvider parameterProvider, string parameterKey, CancellationToken cancellationToken)
    {
        var values = await parameterProvider.GetAsync<TValue>([parameterKey], cancellationToken);
        return values.Count > 0 ? values[parameterKey] : default;
    }
}