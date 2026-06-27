using Mes.Shopfloor.Client.Console.Startup;
using Mes.Shopfloor.Client.ProductionManagement;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Debug;

namespace Mes.Shopfloor.Client.Console;

internal sealed class Program
{
    private static async Task Main(string[] args)
    {
        var builder = new ConsoleAppBuilder(args).UseEntryPoint<HeadlessTerminalEntryPoint>();

        builder.Services.AddTerminalCore(builder.Configuration);
        builder.Services.AddTerminalProductionManagement(builder.Configuration);
        builder.Services.AddLogging(logging =>
        {
            logging.ClearProviders();
            logging.SetMinimumLevel(LogLevel.Trace);
        });
        builder.Services.AddSingleton<ILoggerProvider, DebugLoggerProvider>();

        await builder.Build().RunAsync();
    }
}