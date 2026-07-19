using System.Reflection;
using Mes.Shopfloor.Client.ProductionManagement;
using Mes.Shopfloor.Client.SharedKernel;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Debug;

namespace Mes.Shopfloor.Client.Console;

internal static class DependencyInjection
{
    public static IServiceCollection AddMesShopfloorConsoleClient(this IServiceCollection service, IConfiguration configuration)
    {
        var assemblies = GetAssemblies();

        service.AddLogging(logging =>
        {
            logging.ClearProviders();
            logging.SetMinimumLevel(LogLevel.Trace);
        });
        service.AddSingleton<ILoggerProvider, DebugLoggerProvider>();

        service.AddTerminalCore(configuration, assemblies);
        service.AddTerminalProductionManagement(configuration);

        return service;
    }

    private static Assembly[] GetAssemblies()
    {
        return YieldAssemblyNames().Select(Assembly.Load).ToArray();
    }
    
    private static IEnumerable<string> YieldAssemblyNames()
    {
        yield return "Mes.Shopfloor.Client.SharedKernel";
        yield return "Mes.Shopfloor.Client.ProductionManagement";
    }
}