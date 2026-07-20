using Mes.Shopfloor.Api.SharedKernel.Application.Licensing;
using Mes.Shopfloor.Api.SharedKernel.Application.Parameterization;
using Mes.Shopfloor.Api.SystemManagement.LicenseManagement.Application;
using Mes.Shopfloor.Api.SystemManagement.Parameterization.Application;
using Microsoft.Extensions.DependencyInjection;

namespace Mes.Shopfloor.Api.SystemManagement;

public static class DependencyInjection
{
    public static IServiceCollection AddMesShopfloorSystemManagement(this IServiceCollection services)
    {
        services.AddScoped<ILicenseVerifier, LicenseVerifier>();
        services.AddScoped<IParameterProvider, ParameterProvider>();

        return services;
    }
}