using Mes.Shopfloor.Client.Console.Startup;
using Mes.Shopfloor.Terminal.Core;

namespace Mes.Shopfloor.Client.Console;

internal sealed class Program
{
    private static async Task Main(string[] args)
    {
        var builder = new ConsoleAppBuilder(args).UseEntryPoint<HeadlessTerminalEntryPoint>();

        builder.Services.AddTerminal(builder.Configuration);
        
        await builder.Build().RunAsync();
    }
}