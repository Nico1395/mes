namespace Mes.Shopfloor.Api.SharedKernel.Application.Parameterization;

public interface IParameterProvider
{
    Task<Dictionary<string, TValue?>> GetAsync<TValue>(IEnumerable<string> parameterKeys, CancellationToken cancellationToken);
}