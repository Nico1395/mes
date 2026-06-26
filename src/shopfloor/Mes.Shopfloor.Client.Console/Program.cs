using Mes.Shopfloor.Client.Console.Startup;
using Mes.Shopfloor.Client.ProductionManagement;

namespace Mes.Shopfloor.Client.Console;

internal sealed class Program
{
    private static async Task Main(string[] args)
    {
        var builder = new ConsoleAppBuilder(args).UseEntryPoint<HeadlessTerminalEntryPoint>();

        builder.Services.AddTerminalCore(builder.Configuration);
        builder.Services.AddTerminalProductionManagement(builder.Configuration);

        await builder.Build().RunAsync();
    }
}