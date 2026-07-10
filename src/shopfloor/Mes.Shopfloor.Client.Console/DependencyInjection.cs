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
        service.AddTerminalCore(configuration);
        service.AddTerminalProductionManagement(configuration);
        service.AddLogging(logging =>
        {
            logging.ClearProviders();
            logging.SetMinimumLevel(LogLevel.Trace);
        });
        service.AddSingleton<ILoggerProvider, DebugLoggerProvider>();

        return service;
    }
}