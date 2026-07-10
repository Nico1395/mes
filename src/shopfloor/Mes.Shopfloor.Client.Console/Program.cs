using Mes.Shopfloor.Client.Console.Startup;

namespace Mes.Shopfloor.Client.Console;

internal sealed class Program
{
    private static async Task Main(string[] args)
    {
        var builder = new ConsoleAppBuilder(args).UseEntryPoint<HeadlessTerminalEntryPoint>();

        builder.Services.AddMesShopfloorConsoleClient(builder.Configuration);

        await builder.Build().RunAsync();
    }
}